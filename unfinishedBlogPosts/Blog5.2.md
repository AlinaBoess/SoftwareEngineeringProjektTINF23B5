
# Fünfter Blogeintrag - Fortschritt der letzten Woche

Schön, dass ihr es wieder zu uns geschafft habt :) In dieser Woche konnten wir einige bedeutende Fortschritte in unserem Projekt machen. Besonders stolz sind wir darauf, die erste Demo unserer Webanwendung präsentiert zu haben und mit der Datenbankstruktur einen wichtigen Schritt in der Entwicklung getan zu haben.

## Erste Demo-Webapp und API-Schnittstellen

In dieser Woche haben wir die **API-Schnittstellen** mit ASP.NET erstellt und miteinander verknüpft. Dadurch konnten wir eine erste Demo-Webanwendung entwickeln, die es ermöglicht, nach dem Betätigen eines Buttons eine Tabelle mit den aktuellen Daten der Restaurants zu laden. In dieser Tabelle sind die jeweiligen Adressen und die Tische mit den zugehörigen Plätzen sichtbar.

Natürlich handelt es sich hierbei um eine erste Demo, die noch nicht endgültig ist. Es werden sich noch einige Dinge ändern, aber hier seht ihr die ersten Einblicke:

* **Screenshot 1**: Erste Übersicht.
  <img width="941" alt="SE erste Demo - Tische laden" src="https://github.com/user-attachments/assets/7c487a9d-1140-4294-a04a-8cdeff540bd5">

* **Screenshot 2**: Übersicht der geladenen Restaurants mit Adressen und Tischen.
  <img width="932" alt="SE erste Demo - Tabelle mit Tischen" src="https://github.com/user-attachments/assets/5df11df4-1823-43a0-b9d6-167405ea9cc9">

### Zusammenfassung der Entwicklung

Die Entwicklung der Demo-Webanwendung und der API-Schnittstellen wurde wie folgt aufgeteilt:

- **Lukas und Alex (Backend)**: Erstellung und Verknüpfung der API-Schnittstellen, sowie Implementierung der grundlegenden Logik zur Datenverarbeitung.
- **Alina und Yahya (Frontend)**: Gestaltung der Benutzeroberfläche der Webanwendung und Implementierung der Funktionalitäten zum Abrufen und Anzeigen der Restaurantdaten.
- **Moumen (Datenbanken)**: Einrichtung und Strukturierung der MariaDB-Datenbank, um eine effiziente Speicherung und Verwaltung der Daten zu ermöglichen.

### Technische Herausforderungen und Erkenntnisse

Während der Entwicklung sind wir auf einige technische Herausforderungen gestoßen:

- **Integration der API mit der Frontend-Oberfläche**: Es war eine Herausforderung, die Daten korrekt zwischen der Backend-API und dem Frontend auszutauschen und anzuzeigen.

## Datenbankstruktur

Neben der Webanwendung haben wir in dieser Woche auch die **Datenbankstruktur** fertiggestellt. Wir haben mit MariaDB eine Datenbank angelegt, die alle benötigten Tabellen enthält, um das System effizient zu verwalten. Hier eine Übersicht der wichtigsten Tabellen:

**Users Table**
- ID (Primary Key)
- FirstName
- LastName
- Email
- Password (hashed)
- Role (Customer, Administrator)

**Restaurants Table**
- RestaurantID (Primary Key)
- Name
- Address
- ContactNumber (optional)
- OpeningHours (optional)
- Website (optional)

**Tables Table**
- TableID (Primary Key)
- RestaurantID (Foreign Key)
- TableNr (optional)
- Capacity
- Location (section or area of the restaurant)

**Reservations Table**
- ReservationID (Primary Key)
- ID (Foreign Key)
- TableID (Foreign Key)
- ReservationDateTime
- Status
- CreatedAt
- UpdatedAt (cancel reservation is possible)

**Time Slots Table (Zeitfenster)**
- SlotID (Primary Key)
- Date
- StartTime
- EndTime
- TableID (Foreign Key)
- AvailabilityStatus (Available, Booked)

**Optional Tabellen:**
- **Admin Activity Log Table**
  - LogID (Primary Key)
  - AdminUserID (Foreign Key)
  - ActionType (Add, Modify, Delete)
  - ActionDescription
  - ActionTimestamp

- **Feedback Table**
  - FeedbackID (Primary Key)
  - UserID (Foreign Key referencing Users)
  - ReservationID (Foreign Key)
  - Rating (scale of 1-5 or similar)
  - Comments
  - SubmittedAt

Mit dieser Datenbankstruktur sind wir bestens gerüstet, um die Kernfunktionen des Systems in den kommenden Wochen weiter auszubauen.

## Sonstige Aufgaben

Zusätzlich haben wir diese Woche auch die **weiteren Schritte zur Optimierung der Codequalität** sowie die **Dokumentation** unserer API-Schnittstellen vorangetrieben. Dies hilft uns, die Zusammenarbeit im Team zu verbessern und stellt sicher, dass alle wichtigen Informationen für zukünftige Entwickler verfügbar sind.

Schaut gerne beim nächsten Mal wieder vorbei, wir freuen uns auf euch ;)  
Falls ihr Wünsche oder Anregungen habt, lasst es uns gerne wissen – wir freuen uns auf euer Feedback!

Liebe Grüße  
Lukas, Alex, Moumen, Yahya und Alina.
