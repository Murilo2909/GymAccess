import streamlit as st
from app_utils import req_get, req_post, req_put, req_delete, bytes_to_base64

st.title("Membros")

col1, col2 = st.columns([2, 1])

st.header("Cadastrar membro")
with st.form("register_member"):
    name = st.text_input("Name")
    email = st.text_input("Email")
    cpf = st.text_input("Cpf")
    phone = st.text_input("Phone")
    status = st.selectbox("Status", ["Ativo", "Inativo", "Pago", "Atrasado"], index=0)
    photo = st.file_uploader("Photo (opcional)", type=["jpg", "png", "jpeg"])
    submitted = st.form_submit_button("Cadastrar")
    if submitted:
        photo_b64 = ""
        if photo:
            photo_b64 = bytes_to_base64(photo.read())
        multipart = {
            "Name": name,
            "Email": email,
            "CardId": "1",
            "Cpf": cpf,
            "Phone": phone,
            "Status": status,
            "Photo": photo_b64
        }
        res = req_post(f"https://localhost:5052/api/Member/Register", data=multipart)
        if res.ok:
            st.success("Membro cadastrado")
        else:
            st.error(f"Erro {res.status_code}: {res.text}")

st.header("Lista de membros")
if st.button("Carregar membros"):
    res = req_get("https://localhost:5052/api/Member/BuscarTodos")
    if res.ok:
        st.session_state["members"] = res.json()
    else:
        st.error(f"Erro {res.status_code}: {res.text}")

import base64
import streamlit as st

members = st.session_state.get("members", [])

for m in members:
    mid = m.get("id") or m.get("Id")

    # Converte a foto base64 para bytes
    photo_b64 = m.get("Photo") or m.get("photo")

    if photo_b64:
        # remove o prefixo data:image/...;base64,
        if photo_b64.startswith("data:image"):
            photo_b64 = photo_b64.split(",", 1)[1]

        try:
            photo_bytes = base64.b64decode(photo_b64)
        except Exception as e:
            st.error(f"Erro ao decodificar imagem: {e}")
            photo_bytes = None
    else:
        photo_bytes = None

    # Card container
    st.markdown(
        """
        <div style='padding: 18px; border-radius: 10px; 
                    border: 1px solid #444; margin-bottom: 25px;'>
        """,
        unsafe_allow_html=True
    )

    st.subheader(f"Membro #{mid}")

    # --- LAYOUT COM FOTO ESQUERDA E CAMPOS DIREITA ---
    col_photo, col_data = st.columns([1, 2])

    # FOTO — não editável
    with col_photo:
        if photo_bytes:
            st.image(photo_bytes, caption="Foto do membro", width=220)
        else:
            st.write("Sem foto cadastrada")

    # CAMPOS EDITÁVEIS
    with col_data:
        name = st.text_input(
            "Nome",
            value=m.get("name") or m.get("Name"),
            key=f"name_{mid}"
        )

        email = st.text_input(
            "Email",
            value=m.get("email") or m.get("Email"),
            key=f"email_{mid}"
        )

        phone = st.text_input(
            "Telefone",
            value=m.get("phone") or m.get("Phone"),
            key=f"phone_{mid}"
        )

        status = st.selectbox(
            "Status",
            ["Ativo", "Inativo", "Bloqueado", "Cancelado"],
            index=["Ativo", "Inativo", "Bloqueado", "Cancelado"].index(
                (m.get("status") or m.get("Status") or "Ativo")
            ),
            key=f"status_{mid}"
        )


    # 4 colunas — 2 vazias à esquerda, 2 botões à direita
    colA, colB, colC, col_save = st.columns([2, 2, 1, 1])

    with col_save:
        if st.button("Salvar", key=f"save_{mid}"):
            updated = m.copy()
            updated["name"] = name
            updated["email"] = email
            updated["phone"] = phone
            updated["status"] = status

            res = req_put(f"https://localhost:5052/api/Member/Atualizar/{mid}", json=updated)
            st.success("Atualizado!" if res.ok else f"Erro: {res.text}")

    st.markdown("</div>", unsafe_allow_html=True)
