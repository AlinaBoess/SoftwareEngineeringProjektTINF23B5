# Handout – Restaurant-Reservierungssystem

---

## Projektnamen
**Restaurant-Reservierungssystem**  
Ein webbasiertes System zur digitalen Tischreservierung mit Benutzer- und Adminfunktionen.

---

## Aufwandsstatistiken

### Arbeitsstunden pro Person
| Teammitglied | Arbeitsstunden | Hauptbeitrag                             |
|--------------|---------------|-------------------------------------------|
| Alina        | 55:20 (h,min)    | Frontend-Entwicklung und Dokumentation    |
| Alex         | 52:00 (h,min)    | Backend-Entwicklung & CI/CD               |
| Lukas        | 37:45 (h,min)    | Backend-Entwicklung & Testing             |
| Moumen       | 48:00 (h,min)    | Datenbank & Frontend-Entwicklung          |
| Yahya        | 29:45 (h,min)    | Frontend-Entwicklung & Projektmanagement  |

### Arbeitsstunden pro Workflow
![image](https://github.com/user-attachments/assets/ac768959-f01e-4ecd-a882-4c5d167bac08)


### Arbeitsstunden pro Sprint

![image](https://github.com/user-attachments/assets/9b5ce034-d995-4981-aa25-28be8e0d5d4f)


### Arbeitsstunden pro Person pro Sprint
![image](https://github.com/user-attachments/assets/44722d26-bbc3-4c67-a820-ce265bba0948)

---

## Highlights der Live-Demo

Unsere Live-Demo zeigt zentrale Funktionen unserer Restaurant-Reservierungsplattform anhand ausgewählter Screenshots.

### Startseite
- Begrüßung mit „Willkommen auf unserer Homepage“.
- Von hier aus kann direkt auf die Reservierungsfunktion zugegriffen werden.
- Zusätzlich gibt es oben einen Button, über den Restaurantbesitzer ein Formular zur Registrierung ihres Restaurants aufrufen können.

![Startseite](Home.jpg)

---

### Partner-Restaurant-Übersicht
- Darstellung aller registrierten Partner-Restaurants.
- Einfache Navigation zur Auswahl eines Restaurants.

![Partner-Restaurants](restaurantuebersicht.jpg)

---

### Tischreservierung
- Anzeige einer Tischübersicht mit belegten und freien Tischen.
- Reservierungsformular:
  - Auswahl von Datum und Uhrzeit.
  - Dynamische Anzeige der verfügbaren Tische.
  - Möglichkeit zur Auswahl und Buchung eines freien Tisches.

![Tischreservierung](reservierung.jpg)

---

### Übersicht eigener Reservierungen (User)
- Auflistung der eigenen Reservierungen:
  - Restaurantname, Tischnummer, Kapazität, Datum und Uhrzeit, Website.
- Reservierungen können hier bei Bedarf gelöscht werden.

![Eigene Reservierungen](userReservierungsuebersicht.jpg)

---

### Übersicht der Reservierungen (Restaurant-Owner)
- Restaurantbesitzer sehen alle Reservierungen ihrer Tische:
  - Inklusive Restaurantname, Tischnummer, Kapazität, Datum und Zeitrahmen.
- Es wird nach Restaurants gruppiert, falls der Besitzer mehrere hat.

![Owner-Reservierungsübersicht](ownerReservierungsuebersicht.jpg)

---

### Weitere Funktionen (nicht gezeigt)
- Login für Kund*innen und Restaurantbesitzer.
- Formular zur Restaurantregistrierung.
- Rollenbasierte Zugriffskontrolle im System.


## Highlights unseres Projekts

### Architektur
- Client-Server-Modell mit React-Frontend und C# ASP.NET Core Web API Backend  
- Modulare und skalierbare Struktur  
- Nutzung von Swagger für API-Dokumentation  

### Software Tools / Plattform / Technik / Libraries
- Frontend: React.js, CSS  
- Backend: C# mit ASP.NET Core Web API  
- Datenbank: MariaDB
- Datenbank-Backend Kommunikation: EF Core 
- API-Dokumentation: Swagger  
- Versionskontrolle: Git + GitHub  
- Projektmanagement: Jira  
- Entwicklungsumgebung: Visual Studio 2022  
- CI/CD: GitHub Actions
- Monitoring: Prometheus

### Datenbank Design
- Relationale Tabellen für Benutzer, Tische, Reservierungen und Feedback  
- ACID-konforme Transaktionen und Indexierung zur Optimierung der Performance
#### ER-Diagramm

Dieses ER-Diagramm zeigt die Entitäten und ihre Beziehungen im System.

![ER-Diagramm Draw.io](https://raw.githubusercontent.com/AlinaBoess/SoftwareEngineeringProjektTINF23B5/main/documentation/endPresentation/ER-Diagramm.drawio.png)

#### Datenbankstruktur (aus phpMyAdmin)

Zusätzlich ein generiertes Datenbankschema aus der tatsächlichen Implementierung:

![Datenbankdiagramm](https://raw.githubusercontent.com/AlinaBoess/SoftwareEngineeringProjektTINF23B5/main/documentation/endPresentation/DatabaseDiagramm.png)



### Testing
- Unit Tests zur Validierung einzelner Komponenten
  - Benutzung einer In-Memory Datenbank durch Mocking-Techniken
  - Etwa bei: Authentifizierungs-Service, Reservierungs-Service
- Integration Tests zur Validierung des Gesamtsystems
  - Testen des Systems unter Realbedingungen mit Anfragen anMariaDB-Datenbanksystem
  - Etwa bei: Validierung der Datenbankverfügbarkeit
- Frontend-Tests sind aktuell noch nicht automatisiert umgesetzt

### Metriken

Im Backend werden sowohl Code-Metriken als auch Laufzeit-Metriken erfasst. 

**Code-Metriken:**

![Code Metriken](https://raw.githubusercontent.com/AlinaBoess/SoftwareEngineeringProjektTINF23B5/main/documentation/endPresentation/Code-Metriken.png)

**Laufzeit-Metriken**
Die Laufzeit-Metriken werden mithilfe von Prometheus über die ASP.NET Core Middleware (`prometheus-net.AspNetCore`) erfasst. Sie sind unter dem Endpunkt `/metrics` abrufbar.

Erfasste Metriken sind unter anderem:
- Anzahl der HTTP-Requests pro Endpoint (Request Rate)
- Antwortzeit (Response Time)
- Aktive Verbindungen (Active Connections)


### CI/CD
- CI/CD umgesetzt mit GitHub Actions
- Auslöser: Push auf `main`-Branch
- Schritte:
  - .NET-Umgebung konfigurieren
  - Abhängigkeiten installieren
  - Backend bauen
  - Unit- und Integrationstests ausführen
  - Buildartefakte erzeugen und in das Repository hochladen
- Gesamtlaufzeit: ca. 30–40 Sekunden

---

## Weitere Erfolge und Stolz

- Gelungene Teamarbeit mit offener Kommunikation und klarer Aufgabenverteilung  
- Effektives Projektmanagement mit Jira und Scrum-Methodik  
- Saubere, gut dokumentierte Codebasis  
- Flexible und erweiterbare Systemarchitektur für zukünftige Features  
- Saubere Datenstruktur
---

