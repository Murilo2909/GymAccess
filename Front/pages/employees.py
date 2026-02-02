import streamlit as st
from app_utils import req_post, req_get, req_put

st.title("Funcionários")

st.header("Registrar funcionário")
with st.form("reg_employee"):
    name = st.text_input("Name")
    email = st.text_input("Email")
    password = st.text_input("Password", type="password")
    admin = st.checkbox("Admin")
    submitted = st.form_submit_button("Registrar")
    if submitted:
        payload = {"name": name, "email": email, "password": password, "admin": 1 if admin else 0}
        res = req_post("https://localhost:5052/api/Employee/Register", json=payload)
        if res.ok:
            st.success("Funcionário registrado")
        else:
            st.error(res.text)

st.header("Funcionários")

if st.button("Carregar funcionários"):
    res = req_get("https://localhost:5052/api/Employee/BuscarTodos")
    if res.ok:
        employees = res.json()
        st.session_state["employees"] = employees
        st.success("Funcionários carregados!")
    else:
        st.error(res.text)


employees = st.session_state.get("employees", [])

for i, emp in enumerate(employees, start=1):
    emp_id = emp.get("id") or emp.get("Id")

    # Container do card
    st.markdown(
        """
        <div style='padding: 18px; border-radius: 10px; 
                    border: 1px solid #444; margin-bottom: 25px;'>
        """,
        unsafe_allow_html=True
    )

    st.subheader(f"Funcionário #{i}")

    # Layout igual ao Member: duas colunas
    col_left, col_right = st.columns([1, 2])

    with col_left:
        st.write("📌 Funcionário cadastrado")

    with col_right:
        # Campos editáveis
        name = st.text_input(
            "Nome",
            value=emp.get("name") or emp.get("Name"),
            key=f"name_emp_{emp_id}"
        )

        email = st.text_input(
            "Email",
            value=emp.get("email") or emp.get("Email"),
            key=f"email_emp_{emp_id}"
        )

        # Admin: 0 = não, 1 = sim
        admin_val = emp.get("admin") if emp.get("admin") is not None else emp.get("Admin")
        admin_val = int(admin_val)

        admin = st.selectbox(
            "Administrador?",
            ["Não", "Sim"],
            index=1 if admin_val == 1 else 0,
            key=f"admin_emp_{emp_id}",
            disabled=(i == 1)
        )

        # Active: 0 = inativo, 1 = ativo
        active_val = emp.get("active") if emp.get("active") is not None else emp.get("Active")
        active_val = int(active_val)

        active = st.selectbox(
            "Ativo?",
            ["Inativo", "Ativo"],
            index=1 if active_val == 1 else 0,
            key=f"active_emp_{emp_id}"
        )

    # Botões em linha (igual ao member)
    colA, colB, col_del, col_save = st.columns([2, 2, 1, 1])

    with col_save:
        if st.button("Salvar", key=f"save_emp_{emp_id}"):
            updated = emp.copy()
            updated["name"] = name
            updated["email"] = email
            updated["admin"] = 1 if admin == "Sim" else 0
            updated["active"] = 1 if active == "Ativo" else 0

            res = req_put(f"https://localhost:5052/api/Employee/Atualizar/{emp_id}", json=updated)

            if res.ok:
                st.success("Funcionário atualizado!")
            else:
                st.error(f"Erro: {res.text}")

    st.markdown("</div>", unsafe_allow_html=True)
