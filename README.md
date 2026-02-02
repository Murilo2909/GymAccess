# 🏋️ Gym Access System — Project Overview

This project was developed for a college class and represents a **simplified .NET API** designed to simulate the core structure of a gym management system.

The goal is to demonstrate how a real-world gym system could handle **access control, member management, and facial recognition integration**.

---

## 🚀 Main Features

- Register, update, and list **employees** and **members**
- **Facial embedding extraction** using tensor processing
- **Facial recognition** using `float[128]` embeddings compared via **cosine similarity**
- Functional (yet simple) front-end interface
- **JWT authentication** protecting the entire API (except login)
- Designed to integrate with **hardware devices**, such as gym turnstiles, if needed

---

## 🔐 Access Control Endpoints

Responsible for controlling and verifying gym access.

### **Access**

| Endpoint | Description |
|----------|-------------|
| **VerifyFace** | Receives a base64 image, compares it with registered users, and returns **200 OK** if a match is found |
| **AuthorizeManual** | Verifies if the requester is an admin and returns **200 OK** on success |
| **History** | Retrieves the access history for the gym |

---

## 👨‍💼 Employee Management Endpoints

Responsible for managing employees in the the database.

### **Employee**

| Endpoint | Description |
|----------|-------------|
| **Register** | Receives employee data and registers them in the database |
| **Login** | Receives login credentials and returns a **JWT token** containing user information |
| **BuscarTodos** | Retrieves all gym employees |
| **Atualizar/{id}** | Updates employee information |
| **Deletar/{id}** | Deletes an employee from the database |

---

## 🧍 Member Management Endpoints

Responsible for managing gym members.

### **Member**

| Endpoint | Description |
|----------|-------------|
| **Register** | Receives member data and registers them in the database |
| **BuscarTodos** | Retrieves all gym members |
| **Buscar/{id}** | Retrieves a specific member’s information |
| **Atualizar/{id}** | Updates member information |
| **Deletar/{id}** | Deletes a member from the database |

---

## 🧠 Technical Highlights

- Built primarily with **C# and .NET**
- Uses **facial recognition embeddings** for biometric identification
- Implements **cosine similarity** for face comparison
- Secure authentication using **JWT tokens**
- Structured to be extendable for **real-world hardware integration**
