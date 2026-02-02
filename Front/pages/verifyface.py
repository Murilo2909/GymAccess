import streamlit as st
from app_utils import bytes_to_base64, req_post, SAMPLE_IMAGE_PATH
from io import BytesIO

st.title("Verify Face")

uploaded = st.file_uploader("Selecione uma imagem (jpg/png)", type=["jpg", "jpeg", "png"])

if uploaded is not None:
    if hasattr(uploaded, "read"):
        img_bytes = uploaded.read()
    else:
        uploaded.seek(0)
        img_bytes = uploaded.read()

    st.image(img_bytes, width=300)
    base64_str = bytes_to_base64(img_bytes)

    if st.button("Enviar para /api/Access/VerifyFace"):
        res = req_post(f"{st.session_state.get('API_BASE', 'https://localhost:5052')}/api/Access/VerifyFace", data={"base64Photo": base64_str})
        if res.ok:
            try:
                st.success("200 OK")
                st.json(res.json())
            except Exception:
                st.write(res.text)
        else:
            st.error(f"{res.status_code}: {res.text}")
# ------------------------------------------------------------
# BOTÃO LIBERAR MANUALMENTE
# ------------------------------------------------------------
if st.button("Liberar Manualmente (AuthorizeManual)"):
    res = req_post(
        f"{st.session_state.get('API_BASE', 'https://localhost:5052')}/api/Access/AuthorizeManual",
        json={}  # caso precise enviar info depois
    )

    if res.ok:
        st.success("Entrada autorizada manualmente!")
        try:
            st.json(res.json())
        except:
            st.write(res.text)
    else:
        st.error(f"{res.status_code}: {res.text}")