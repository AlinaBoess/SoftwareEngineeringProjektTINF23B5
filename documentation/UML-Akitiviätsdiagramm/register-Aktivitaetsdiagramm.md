```mermaid
---
title: Nutzerregistrierung Webseite
---
stateDiagram
    state if_state <<choice>>
    [*] --> EingabeRegistrierungsdaten: Start
    EingabeRegistrierungsdaten --> PrüfeDaten: Daten eingegeben
    PrüfeDaten --> if_state
    if_state --> Fehlermeldung: Validierung fehlgeschlagen
    if_state --> AccountErstellen: Validierung erfolgreich
    Fehlermeldung --> EingabeRegistrierungsdaten: Fehler beheben
    AccountErstellen --> BestätigungAnzeigen: Account erfolgreich erstellt
    BestätigungAnzeigen --> [*]: Registrierung abgeschlossen
