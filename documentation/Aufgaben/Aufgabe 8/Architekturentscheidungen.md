# Zusammenfassung der Architekturentscheidungen und Entwurfsmuster

## Architektonisch bedeutsame Anforderungen (ASR)

### Leistungsfähigkeit (Performance)
- **Anforderung:** Die Web-App muss schnell und zuverlässig sein, auch bei hoher Benutzerlast.
- **Details:**
  - Abwicklung paralleler Tischreservierungen ohne Fehler.
  - Effiziente und fehlerfreie Transaktionen in Echtzeit.
- **Taktik:**
  - Lasttests und Optimierung der Datenbankabfragen.

### Sicherheit
- **Anforderung:** Sichere Speicherung und Verarbeitung von Benutzerdaten.
- **Details:**
  - Verschlüsselte Speicherung gemäß DSGVO.
  - Schutz vor unautorisiertem Zugriff und Manipulation.
- **Taktik:**
  - Rollen- und Berechtigungssystem implementieren.

### Zuverlässigkeit
- **Anforderung:** Keine Doppelbuchungen oder fehlerhaften Buchungsbestätigungen.
- **Details:**
  - Datenbankkonsistenz bei Transaktionen.
- **Taktik:**
  - Einsatz von Transaktionssperren.

### Benutzerfreundlichkeit (Usability)
- **Anforderung:** Intuitive Benutzeroberfläche, auch für unerfahrene Nutzer.
- **Details:**
  - Klare Fehlermeldungen bei falschen Eingaben.
- **Taktik:**
  - Usability-Tests und Benutzerfeedback nutzen.

### Wartbarkeit
- **Anforderung:** Einfaches und flexibles Implementieren von Änderungen.
- **Details:**
  - Modularisierung des Codes.
- **Taktik:**
  - Versionierung.

---

## Qualitätsmerkmale und Analyse
Die Sammlung und Analyse erfolgt durch folgende Methoden:
1. **Lasttests:** Simulation von hoher Benutzerlast.
2. **Fehleranalyse:** Testen von Netzwerkabbrüche o.a .
3. **Integrationstests:** Überprüfung des Zusammenspiels von Frontend und Backend.
4. **Sicherheitstests:** Schutz vor Angriffen wie SQL-Injection.

---

## Architekturstile/-muster

### Client-Server-Modell
- **Beschreibung:** Klassisches Modell mit separatem Backend (C#) für die Geschäftslogik und Frontend (HTML, CSS, JavaScript) für die Benutzeroberfläche.

### Schichtenarchitektur (Layered Architecture)
- **Präsentationsschicht:** Frontend.
- **Logikschicht:** Backend mit C#.
- **Datenzugriffsschicht:** Datenbankzugriff (SQL-Server).
---

## Taktiken zur Umsetzung

1. **Testgetriebene Entwicklung (TDD):**
   - Unit-Tests decken jede neue Funktion ab, minimieren Fehler.

2. **Continuous Integration/Continuous Deployment (CI/CD):**
   - Automatisiertes Testen und Bereitstellen neuer Versionen.

3. **Skalierbarkeit durch Caching:**
   - Reduzierung der Serverlast durch Caching häufig abgerufener Daten.

4. **Datenbankoptimierung:**
   - Nutzung von Indizes für Abfragen.
   - Sicherstellung von Transaktionssicherheit (ACID-Prinzip).

5. **Überwachung:**
   - Monitoring-Tools für Leistung und Zuverlässigkeit.

---


| **Quality Attribute** | **Refinement**              | **Quality Attribute Scenarios**                                                                 | **Business Value** | **Technical Risk** |
|------------------------|-----------------------------|--------------------------------------------------------------------------------------------------|---------------------|---------------------|
| **Performance**        | Response time              | Ein Benutzer bucht einen Tisch zur Stoßzeit, und die Transaktion wird in weniger als 2 Sekunden abgeschlossen. | Hoch                | Mittel              |
|                        | Throughput                 | Während der Stoßzeit verarbeitet das System mindestens 100 Buchungen pro Minute ohne Verzögerung. | Mittel              | Mittel              |
| **Usability**          | Simplicity of Interface    | Ein neuer Mitarbeiter kann nach 2 Stunden Schulung die Reservierungsverwaltung problemlos bedienen. | Mittel              | Niedrig             |
|                        | Error-Free Transactions    | Ein Benutzer reserviert einen Tisch und erhält bei fehlerhafter Eingabe sofort Feedback ohne Systemabsturz. | Hoch                | Mittel              |
| **Maintainability**    | Routine Updates            | Ein Entwickler kann Änderungen an der Backend-API vornehmen und innerhalb von zwei Tagen deployen. | Hoch                | Mittel              |
|                        | Frontend Adaptability      | Änderungen am Design (CSS/HTML) können ohne Backend-Anpassungen vorgenommen werden.              | Mittel              | Mittel              |
| **Security**           | Authorization              | Nur autorisierte Mitarbeiter können Reservierungsdetails bearbeiten.                             | Hoch                | Hoch                |
|                        | Data Protection            | Benutzerdaten werden verschlüsselt gespeichert, um DSGVO-Standards zu erfüllen.                 | Hoch                | Hoch                |
