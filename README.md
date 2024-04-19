Proiect realizat in .NET 8.0.

Contine HTTP requests pentru generearea si validarea unui OTP.
Primul http request este un get in care se genereaza un otp de 8 caractere, se cripteaza cu AES si se trimite spre frontend. Al doilea este un post care primeste o parola criptata de la frontend, o decripteaza si verifica daca se potriveste cu cea salvata.
Parola salvata este mentinuta pentru 30 de secunde, dupa care trebuie regenerata.
Pe langa cele 2 http request-uri, mai exista un Task care asteapta 30 de secunde si dupa sterge parola salvata.
Au fost implementate si 2 unit teste, unul pentru get in care verifica daca parola generata are lungimea buna, si unul pentru post in care se verifica daca rezultatul este bun in cazul in care inputul venit de la frontend si parola salvata sunt bune.