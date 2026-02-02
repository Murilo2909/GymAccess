# Project intention is for a collage class i was having
# It is a simple dotnet program/api that is intended to emulate the "skeleton" of what a gym systen should do

# Some of it's feature are:
# -Register/update/list employees and members
# -Facial embedding extraction using tensors
# -Facial recognition usind a float[128] list and compare it using cosine similarity
# -Functional, yet simple front-end
# -JWT token verification on the whole api except login 

# Because the system is made using mostly c#, it's able to, if needed, integrate the facial recognition service with a hardware, like a gym's turnstile

# the program has the following endpoits:

# Used to controll and verify accesses in the user's gym
# -Access: 
#   -VerifyFace (recieve a base64 image and compares with users on DB retrieving code 200 on success)
#   -AuthorizeManual (verifies if user is admin, retrieving code 200 on success)
#   -History (retrieves access history of the user's gym)

#  Used to controll employee on DB
# -Employee: 
#   -Register (recieve employee's data and register him in the DB returning code 200 on success)
#   -Login (recive login data and return JWT token containing user info)
#   -BuscarTodos (retrive's all employees on the gym)
#   -Atualizar/{id} (updates employee info)
#   -Deletar/{id} (deletes employee from DB)

#  Used to controll Members on DB
# -Member: 
#   -Register (recieve Member's data and register him in the DB returning code 200 on success)
#   -BuscarTodos (retrive's all members on the gym)
#   -Buscar/{id} (retrive's user information if found on DB)
#   -Atualizar/{id} (updates member info)
#   -Deletar/{id} (deletes member from DB)


