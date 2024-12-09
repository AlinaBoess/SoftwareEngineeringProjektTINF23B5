# Overall Use-Case Diagram

```mermaid
graph TD
    User -->|Interagiert mit| System
    Admin -->|Verwaltet| System

    System -->|Bietet an| Registrierung
    System -->|Bietet an| Reservierung
    System -->|Ermöglicht| Benutzerkontenverwaltung
    System -->|Generiert| Berichte
    System -->|Unterstützt| Benachrichtigungen

    subgraph UseCases
        Registrierung -->|Erfordert| Validierung
        Registrierung -->|Nutzt| Datenbank

        Reservierung -->|Erfordert| Tischverfügbarkeit
        Reservierung -->|Speichert| Reservierungsdaten

        Berichte -->|Bezieht Daten aus| Reservierungen
        Berichte -->|Bezieht Daten aus| Benutzerkonten
    end

    User -->|Fordert an| Reservierung
    User -->|Fordert an| Registrierung
    Admin -->|Generiert| Berichte
