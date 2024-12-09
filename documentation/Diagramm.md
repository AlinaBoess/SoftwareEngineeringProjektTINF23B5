# Overall Use-Case Diagram

```mermaid
graph TD
    %% Hauptakteure
    User[Benutzer] -->|Interagiert mit| System[System]
    Admin[Administrator] -->|Verwaltet| System

    %% Hauptfunktionen des Systems
    System -->|Bietet an| Registrierung
    System -->|Bietet an| Reservierung
    System -->|Ermöglicht| Benutzerkontenverwaltung
    System -->|Generiert| Berichte
    System -->|Unterstützt| Benachrichtigungen

    %% Detaillierte Use-Cases
    subgraph UseCases[Use-Cases]
        Registrierung -->|Erfordert| Validierung
        Registrierung -->|Nutzt| Datenbank

        Reservierung -->|Erfordert| Tischverfügbarkeit
        Reservierung -->|Speichert| Reservierungsdaten

        Berichte -->|Bezieht Daten aus| Reservierungen
        Berichte -->|Bezieht Daten aus| Benutzerkonten
    end

    %% Interaktionen der Akteure
    User -->|Fordert an| Reservierung
    User -->|Fordert an| Registrierung
    Admin -->|Generiert| Berichte
