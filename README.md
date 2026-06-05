# Task Management System

## Descriere

Task Management System este o aplicație web dezvoltată pentru gestionarea activităților și organizarea task-urilor. Aplicația permite utilizatorilor să creeze, editeze, șteargă, caute și marcheze task-uri ca finalizate.

## Funcționalități

* Creare task
* Vizualizare task-uri
* Editare task
* Ștergere task
* Căutare task-uri după titlu
* Marcare task-uri ca finalizate / nefinalizate
* Persistența datelor utilizând Firebase Firestore

## Tehnologii utilizate

### Frontend

* Angular
* TypeScript
* HTML
* CSS

### Backend

* ASP.NET Core Web API
* C#

### Bază de date

* Firebase Firestore

### DevOps

* GitHub
* GitHub Actions
* CodeQL
* xUnit

## Arhitectură

Aplicația utilizează o arhitectură client-server:

* Frontend Angular
* Backend ASP.NET Core Web API
* Firebase Firestore pentru stocarea datelor

## Testare

Proiectul include teste unitare dezvoltate folosind xUnit.

Exemple de teste:

* Verificarea valorilor implicite ale unui Task
* Verificarea proprietății Title
* Verificarea proprietății Description
* Verificarea datei de creare

## Continuous Integration

Pipeline-ul GitHub Actions execută automat:

1. Restore dependencies
2. Build project
3. Run unit tests
4. Static Code Analysis (CodeQL)

