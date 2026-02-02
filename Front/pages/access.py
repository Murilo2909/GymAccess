import base64
import streamlit as st
from app_utils import req_get

st.header("Histórico de Acessos")

if st.button("Carregar histórico"):
    res = req_get("https://localhost:5052/api/Access/History")
    if res.ok:
        st.session_state["history"] = res.json()
        st.success("Histórico carregado!")
    else:
        st.error(res.text)


history = st.session_state.get("history", [])

for h in history:
    member = h.get("member")
    time = h.get("time")
    employee_id = h.get("employeeId")

    # Cor do card
    # Azul = teve funcionário (manual)
    # Verde = automático (reconhecimento)
    color = "#3498db" if employee_id else "#2ecc71"

    # ---- Foto ----
    photo_b64 = member.get("photo") if member else None
    photo_bytes = None

    if photo_b64:
        if photo_b64.startswith("data:image"):
            photo_b64 = photo_b64.split(",", 1)[1]
        try:
            photo_bytes = base64.b64decode(photo_b64)
        except:
            photo_bytes = None

    # ---- Início do Card ----
    st.markdown(
        f"""
        <div style='padding: 15px; 
                    border-radius: 10px; 
                    border: 2px solid {color}; 
                    margin-bottom: 20px;
                    background-color: #1e1e1e;'>
        """,
        unsafe_allow_html=True,
    )

    st.markdown(
        f"<h4 style='color:{color};margin-bottom:5px;'>Acesso — {'Manual' if employee_id else 'Automático'}</h4>",
        unsafe_allow_html=True
    )

    # Layout: Foto + dados
    col_photo, col_data = st.columns([1, 2])

    with col_photo:
        if photo_bytes:
            st.image(photo_bytes, width=200)
        else:
            st.write("Sem foto")

    with col_data:
        st.markdown(f"**Nome:** {member.get('name') if member else '-'}")
        st.markdown(f"**Status:** {member.get('status') if member else '-'}")
        st.markdown(f"**Data/Hora:** {time}")
        st.markdown(f"**Employee Nome:** {h.get("employeeName") if employee_id else 'Nenhum (Reconhecimento Facial)'}")

    st.markdown("</div>", unsafe_allow_html=True)
