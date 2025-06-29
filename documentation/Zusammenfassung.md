#  Restaurant-Tischreservierung – Gruppenprojekt TINF23B5

## 1. Projekthintergrund & Vision

Im Rahmen unseres DHBW-Kurses *TINF23B5* haben wir ein web-basiertes [Restaurant-Rreservierungssystem](https://github.com/AlinaBoess/SoftwareEngineeringProjektTINF23B5) für Restaurants entwickelt. 
Ziel ist es, dass Nutzer:

- ein Restaurant auswählen,
- einen Tisch im Raumplan sehen und auswählen,
- Reservierungen vornehmen und stornieren,
- Feedback zu ihrem Besuch geben.

Besonderes Augenmerk legen wir auf **Benutzerfreundlichkeit**, **Performance**, **Sicherheit** und **Skalierbarkeit**.
Zur effektiven Verwaltung unseres Projekts haben wir uns dazu entschieden, die Aufgaben- und Rollenverteilung über ein [Jira](https://reserve4resturant.atlassian.net/jira/software/projects/SCRUM/boards/1)-Board vorzunehmen, was den agilen Entwicklungsprozess als Team signifikant vereinfacht hat.

---

## 2. Architektur (SAD)

Unser [**System Architecture Document (SAD)**](https://github.com/AlinaBoess/SoftwareEngineeringProjektTINF23B5/blob/main/documentation/architecture/SAD.md) beschreibt im Wesentlichen die folgende Projektstruktur:

- **Backend**: Implementiert mit ASP.NET Core (C#), in drei Schichten (Controller → Services → Repositories).
- **Datenbankzugriff**: Über Entity Framework Core für effizientes ORM.
- **REST-APIs**: CRUD-Endpunkte für Restaurants, Räume, Tische, Reservierungen und Feedback.
- **Qualitätsmerkmale**: TLS-Verschlüsselung, rollenbasierte Zugriffssteuerung (RBAC), Caching, Logging und Monitoring, Clean Code Patterns.

Diese modulare Architektur sorgt für Wartbarkeit, Wiederverwendbarkeit und klare Verantwortlichkeiten.

---

## 3. Anforderungen (SRS)

In den [Software Requirements Specification (SRS)](https://github.com/AlinaBoess/SoftwareEngineeringProjektTINF23B5/blob/main/documentation/SRS/SoftwareRequirementDocumentation.md) sind unsere Projektanforderungen detailliert beschrieben:

- **Stakeholder**: Restaurantinhaber, Kunden, Administratoren.
- **Use Cases**: Reservierung, Stornierung, Feedback, Nutzer-Authentifizierung.
- **Funktionale Anforderungen**: Authentifizierung, dynamischer Tischplan, Feedbacksystem.
- **Nicht-funktionale Anforderungen**: Sicherheit, Performance, Skalierbarkeit, Benutzerfreundlichkeit, Barrierefreiheit.
- **Datenmodell**: Entitätsdiagramme und Tabellenstrukturen für Nutzer, Restaurants, Räume, Tische, Reservierungen, Feedback.

Damit ist sowohl die funktionale als auch die technische Basis sauber dokumentiert.

---

## 4. Risikomanagement (RMMM)

Im Rahmen unserer [Risiko-Management-Bewertung](https://github.com/AlinaBoess/SoftwareEngineeringProjektTINF23B5/blob/main/documentation/RMMM_Risiken_Restaurantprojekt.xlsx) haben wir zentrale Projekt-Risiken definiert und adressiert:

| Risiko                      | Vorgehensweise                                           |
|----------------------------|----------------------------------------------------------|
| Backend-Verzögerung        | Backend ist fertiggestellt                               |
| Sicherheitslücken          | TLS, Passwort-Hashing, RBAC implementiert                |
| Performance-Probleme       | Caching und Monitoring vorbereitet                      |
| Technische Schulden        | Regelmäßige Code Reviews und Modultests etabliert       |

Wie obig ersichtlich, wurden alle identifizierten Risiken strukturiert abgearbeitet, wobei vor allem das Backend vollständig abgeschlossen ist.

---

## 5. Projektverlauf & Architekturentscheidungen

Bereits in unserem [7. Blogpost](https://github.com/AlinaBoess/SoftwareEngineeringProjektTINF23B5/discussions/7) haben wir folgende wichtigen Architektur-Merkmale unseres Projekts beschrieben:

1. **Usability**: responsives UI, klare Fehlermeldungen  
2. **Performance**: effiziente Datenbankzugriffe, Caching  
3. **Security**: TLS, Authentifizierung, autorisierte Zugriffe  
4. **Skalierbarkeit**: Cloud-fähiges Design  
5. **Datenmodellierung**: ER-Struktur für Erweiterbarkeit  
6. **Clean Code**: Rollenbasierte Klassenstruktur  
7. **Scrum-Prozess**: Sprintplanung, Jira, regelmäßige Reviews

---

## 6. Deployment

Im Rahmen des Entwicklungsprozesses haben wir eine automatisiert ausgelöste CI/CD-Pipeline für den `main`-Branch unseres Projektes eingerichtet. Damit ist unser Projekt nun in der Lage, automatisierte Builds, Tests und Deployments durchzuführen, wodurch eine stets hohe Qualität unseres Produkts sichergestellt wird.
Dabei werden folgende Aktionen ausgeführt:

![image](https://github.com/user-attachments/assets/7e54decd-27de-4361-957f-f7c94c87f221)

---

## 7. Aktueller Entwicklungsstand des Projekts 

- **Backend & Datenbank**  
  - Vollständig implementiert; stabile REST-API-Endpunkte  
  - [Normiertes DB-Schema](https://github.com/AlinaBoess/SoftwareEngineeringProjektTINF23B5/blob/main/documentation/endPresentation/DatabaseDiagramm.png) für alle Entitäten (User, Restaurant, Tisch, Reservierung, Feedback)

- **Frontend**  
  - Nahezu abgeschlossenes [Frontend](https://github.com/AlinaBoess/SoftwareEngineeringProjektTINF23B5/blob/main/documentation/endPresentation/Home.jpg) mit Verwendung von React-basierten UI-Komponenten innerhalb des Next.js-Framework
  - Raumplan, Formulare und API-Konnektivität sind vollständig implementiert

Der komplette Backend-Stack steht bereit; das Frontend ist ebenfalls nahezu abgeschlossen.

---

## 8. Qualitätsbericht

Um die korrekte Funktionsweise bestehender und neuer Funktionalitäten stets sicherzustellen, wurden im Rahmen des in unserem [13. Blogpost](https://github.com/AlinaBoess/SoftwareEngineeringProjektTINF23B5/discussions/17) beschriebenen [Testplans](https://github.com/AlinaBoess/SoftwareEngineeringProjektTINF23B5/blob/main/documentation/Testplan.md) sowohl Unit- als auch Integrationstests für unsere Anwendnung realisiert.
Diese Herangehensweise stellt eine wertvolle Maßnahme zur Entwicklung qualitativ hochwertiger, zuverlässiger Software dar.

Zur Laufzeit-Evaluation unserer Anwendung wurden im Rahmen unseres [15. BLogposts](https://github.com/AlinaBoess/SoftwareEngineeringProjektTINF23B5/discussions/22) einige relevante Metriken definiert und erläutert, welche während des Debug-Betriebs unserer Anwendung gesammelt werden.
Dies umfasst zudem Code-Metriken: ![](https://github.com/AlinaBoess/SoftwareEngineeringProjektTINF23B5/blob/main/documentation/endPresentation/Code-Metriken.png)

Im Rahmen unserer Qualitätssicherungsmaßnahmen wurde zudem ein Technical Review durchgeführt, wobei neben den Beurteilungen eigener Te4ammitglieder auch die Expertise einer externen Entwicklerin betrachtet wurde.
Die Ergebnisse dieses Meetings wurden in unserem [vorletzten Blogeintrag](https://github.com/AlinaBoess/SoftwareEngineeringProjektTINF23B5/discussions/27) ausführlich dargelegt.


---


## 9. Projektretrospektive

In unserem [letzten Blogeintrag](https://github.com/AlinaBoess/SoftwareEngineeringProjektTINF23B5/discussions/28) haben wir die Ergebnisse unserer Projektretrospektive aufgezeigt; dabei ließen sich diese wesentlichen Erkenntnisse ziehen:

- Positive Aspekte
    - entspannte und offene Teamatmosphäre
    - konstruktives Feedback ist konstruktiv
    - gelungene, faire Aufgabenverteilung

- Herausforderungen/Verbesserungspotential
    - teils unklare Kommunikation
    - verbesserungswürdige Projektstruktur auf GitHub (Naming Conventions)

- Learnings und Ausblick
    - Tech Stack frühzeitig überdenken und ggf. anpassen
    - Mehr Rücksprachen zur Struktur und Organisation

---

Wir befinden uns momentan der finalen Phase. Der Fokus liegt jetzt darauf, alles Frontend-Elemente zu integrieren, das System umfassend zu testen und fertig für den Einsatz zu machen. 
Gerne könnt ihr euch bei weiterem Interesse auch noch einmal unsere [Projektpräsentation](https://github.com/AlinaBoess/SoftwareEngineeringProjektTINF23B5/blob/main/documentation/endPresentation/Projektvorstellung.pptx), sowie das zugehörige [Projekthandout](https://github.com/AlinaBoess/SoftwareEngineeringProjektTINF23B5/blob/main/documentation/endPresentation/Handout.md) mit detaillierten Infografiken, Screenshots der Anwendung und weiterführenden Details anschauen.
Vielen Dank für euer Interesse an unserem Projekt!
