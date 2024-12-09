# Komponentendiagramm - Restaurant-Tischreservierungssystem

## Beschreibung

Das Komponentendiagramm zeigt die wichtigsten Bestandteile des **Restaurant-Tischreservierungssystems** sowie deren Interaktionen:

### Hauptkomponenten

1. **Frontend**  
   - Stellt die Benutzeroberfläche bereit, über die Nutzer Tische reservieren und ihre Konten verwalten können.  
   - Realisiert mit HTML, CSS und JavaScript. Kommuniziert über HTTP-Requests mit der API.

2. **API**  
   - Vermittelt zwischen Frontend und Backend.  
   - Stellt Restful-Endpunkte bereit, um Aktionen wie Reservierungen, Authentifizierungen und Datenabfragen zu ermöglichen.

3. **Backend**  
   - Enthält die Geschäftslogik und verarbeitet API-Anfragen.  
   - Verantwortlich für die Validierung und Implementierung von Kernfunktionen.

4. **Datenbank**  
   - Speichert alle Systemdaten, wie Benutzerinformationen, Reservierungen und Restaurantdaten.  
   - Unterstützt CRUD-Operationen (Erstellen, Lesen, Aktualisieren, Löschen) über das Backend.

5. **Authentifizierungssystem**  
   - Sichert die Benutzeridentität durch Token-basierte Authentifizierung.  
   - Kommuniziert mit der API und der Datenbank.

### Beziehungen zwischen Komponenten
- **Frontend**: Sendet Anfragen an die **API**, um Benutzeraktionen wie Reservierungen auszuführen.  
- **API**: Interagiert mit dem **Backend** und leitet Datenanfragen weiter.  
- **Backend**: Greift direkt auf die **Datenbank** zu, um Anfragen zu bearbeiten.  
- **Authentifizierungssystem**: Validiert Benutzeranmeldungen und greift auf die Benutzerdatenbank zu.  

---

## Komponentendiagramm in Mermaid
![Komponentendiagramm](https://github.com/user-attachments/assets/86d87b2a-62b0-49a5-b4c2-6f8776575214)

```mermaid
graph LR
    Frontend["Frontend\n(Web Interface)"] --> API["API\n(Restful Endpoints)"]
    API --> Backend["Backend\n(Server Logik)"]
    Backend --> Database["Datenbank\n(MySQL)"]
    API --> Auth["Authentifizierung\n(Benutzer-Login)"]
    Auth --> Database
