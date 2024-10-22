```mermaid
---
title: Zustandsdiagramm Nutzerregistrierung Webseite
---
stateDiagram
    [*] --> EingabeRegistrierungsdaten: Start
    EingabeRegistrierungsdaten --> PrüfeDaten: Daten eingegeben
    PrüfeDaten --> Fehlermeldung: Validierung fehlgeschlagen
    Fehlermeldung --> EingabeRegistrierungsdaten: Fehler beheben
    PrüfeDaten --> AccountErstellen: Validierung erfolgreich
    AccountErstellen --> BestätigungAnzeigen: Account erfolgreich erstellt
    BestätigungAnzeigen --> [*]: Registrierung abgeschlossen
