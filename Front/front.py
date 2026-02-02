import streamlit as st
from app_utils import API_BASE, SAMPLE_IMAGE_PATH, req_post, set_token, get_token, req_get

st.set_page_config(page_title="GymAccess - Front", layout="wide")

st.title("GymAccess — Login")

# ===============================
# LOGIN NA TELA PRINCIPAL
# ===============================
if not get_token():

    st.subheader("Login")

    email = st.text_input("Email")
    password = st.text_input("Senha", type="password")

    if st.button("Entrar"):
        try:
            res = req_post(f"{API_BASE}/api/Employee/Login",
                           json={"email": email, "password": password})

            if res.ok:
                data = res.json()
                token = data.get("token") or data.get("Token") or data.get("access_token")

                if token:
                    set_token(token)
                    st.success("Login realizado com sucesso!")
                    st.rerun()
                else:
                    st.error("A resposta não contém um token válido.")

            else:
                st.error(f"Erro {res.status_code}: {res.text}")

        except Exception as e:
            st.error(f"Erro ao conectar: {e}")

    # Para a execução aqui caso não esteja logado
    st.stop()

# ===============================
# SE CHEGOU AQUI, ESTÁ LOGADO
# ===============================

st.success("Você está logado!")

st.write("API_BASE:")
st.code(API_BASE)

st.write("Sample image (local):")
st.write(SAMPLE_IMAGE_PATH)
