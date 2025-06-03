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
| Alina        | XX Stunden    | Frontend-Entwicklung und Dokumentation    |
| Alex         | XX Stunden    | Backend-Entwicklung & CI/CD               |
| Lukas        | XX Stunden    | Backend-Entwicklung & Testing             |
| Moumen       | XX Stunden    | Datenbank & Frontend-Entwicklung          |
| Yahya        | XX Stunden    | Frontend-Entwicklung & Projektmanagement  |

### Arbeitsstunden pro Workflow
(Grafik einfügen)

### Arbeitsstunden pro Sprint
(Grafik einfügen)

### Arbeitsstunden pro Person pro Sprint
(Grafik einfügen)

---

## Highlights unserer Demo

**TODO**

---

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

