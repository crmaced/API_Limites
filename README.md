# API Limites
Esta API tem como finalidade alimentar as bases de dados de clientes e seus limites de crédito (Limite Total, Tomado e Disponível).

**Consultas: Clientes e Saldos**
----
  Retorna dados json sobre todos os usuários e seus respectivos saldos.

* **URL**: /api/Consultas/Clientes e Saldos
* **Method:** `GET`
*  **URL Params:** None
* **Data Params:** None
* **Success Response:**

  * **Code:** 200 <br />
    **Content:** `{ "cD_Cliente": 1, "nome_RazaoSocial": "Carla Pereira Fagundes", "iD_Tipo": 1, "tipo": "Pessoa Física", "cpF_CNPJ": "45639089083", "conta": "00001", "email": "carla_pereira@hotmail.com", "iD_Status": 1, "status": "Ativo", "limite_Total": 0, "limite_Tomado": 0, "limite_Disponivel": 0 }`
 
 <br>
 
 **Consultas: Clientes**
----
  Retorna dados json sobre todos os usuários.

* **URL**: /api/Consultas/Clientes
* **Method:** `GET`
*  **URL Params:** None
* **Data Params:** None
* **Success Response:**

  * **Code:** 200 <br />
    **Content:** `{ "cD_Cliente": 1, "nome_RazaoSocial": "Carla Pereira Fagundes", "iD_Tipo": 1, "tipo": "Pessoa Física", "cpF_CNPJ": "45639089083", "conta": "00001", "email": "carla_pereira@hotmail.com", "iD_Status": 1, "status": "Ativo" }`
 
 <br>
 
 **Consultas: Cliente {id}**
----
  Retorna dados json sobre um usuário específico.

* **URL**: /api/Consultas/{id}
* **Method:** `GET`
*  **URL Params:**
 **Required:** `{id} = id cliente`
* **Data Params:** None
* **Success Response:**

  * **Code:** 200 <br />
    **Content:** `{ "cD_Cliente": 1, "nome_RazaoSocial": "Carla Pereira Fagundes", "iD_Tipo": 1, "tipo": "Pessoa Física", "cpF_CNPJ": "45639089083", "conta": "00001", "email": "carla_pereira@hotmail.com", "iD_Status": 1, "status": "Ativo", "limite_Total": 0, "limite_Tomado": 0, "limite_Disponivel": 0 }`
    
* **Error Response:**

  * **Code:** 200 <br />
    **Content:** `"Cliente não cadastrado."`
