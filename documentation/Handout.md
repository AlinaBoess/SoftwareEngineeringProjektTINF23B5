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
- API-Dokumentation: Swagger  
- Versionskontrolle: Git + GitHub  
- Projektmanagement: Jira  
- Entwicklungsumgebung: Visual Studio 2022  
- CI/CD: GitHub Actions
- Monitoring: Prometheus

### Datenbank Design
- Relationale Tabellen für Benutzer, Tische, Reservierungen und Feedback  
- ACID-konforme Transaktionen und Indexierung zur Optimierung der Performance
(ER Diagramm!)

### Testing
- Unit Tests zur Validierung einzelner Komponenten
  - Benutzung einer In-Memory Datenbank durch Mocking-Techniken
  - Etwa bei: Authentifizierungs-Service, Reservierungs-Service
- Integration Tests zur Validierung des Gesamtsystems
  - Testen des Systems unter Realbedingungen mit Anfragen anMariaDB-Datenbanksystem
  - Etwa bei: Validierung der Datenbankverfügbarkeit
- Frontend-Tests sind aktuell noch nicht automatisiert umgesetzt

### Metriken

Erfasst mit Prometheus über die ASP.NET Core Middleware (`prometheus-net.AspNetCore`), abrufbar unter `/metrics`.

**Verwendete Metriken:**
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
- Umsetzung strenger Datenschutz- und Sicherheitsanforderungen (DSGVO-konform)  

---

