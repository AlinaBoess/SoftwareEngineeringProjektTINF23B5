### **Erster Schritt: Klärung der Anforderungen an Qualitätsmerkmale**
Dazu wird die **6-Part Form** verwendet:
1. **Stimulusquelle**: Wer oder was initiiert die Anforderung? (z. B. Benutzer, Administratoren, oder externe Systeme).
2. **Stimulus**: Was wird verlangt oder ausgelöst? (z. B. Tischreservierung, Feedback abgeben).
3. **Artefakt**: Welche Komponente ist betroffen? (z. B. Datenbank, Frontend, API).
4. **Umgebung**: In welchem Betriebszustand befindet sich das System? (z. B. normaler Betrieb, hoher Nutzerandrang).
5. **Reaktion**: Wie reagiert das System auf die Anforderung? (z. B. Anzeige einer Reservierungsübersicht).
6. **Messung der Reaktion**: Wie wird die Qualität geprüft? (z. B. Ladezeit ≤ 2 Sekunden).

### **Zweiter Schritt: Diskussion potenzieller Taktiken**
Es werden Architekturentscheidungen getroffen, um die identifizierten Anforderungen zu erfüllen:
- **Usability**: Responsive Design und klare Fehlermeldungen werden implementiert, um Benutzerfreundlichkeit zu gewährleisten.
- **Performance**: Optimierung der Datenbankzugriffe und Verwendung von Caching, um Antwortzeiten zu minimieren.
- **Sicherheit**: TLS-Verschlüsselung für Datenübertragungen und rollenbasierte Zugriffsrechte für Benutzer und Administratoren werden eingeführt.
- **Skalierbarkeit**: Einsatz einer Cloud-Hosting-Lösung, um bei Spitzenlasten zusätzliche Serverkapazitäten bereitzustellen.

### **Dritter Schritt: Dokumentation der Architekturentscheidungen**
Die Architekturentscheidungen und ihre Begründungen werden dokumentiert:
- **Datenbankstruktur**: Die Beziehungstabellen (z. B. für Benutzer, Tische, und Reservierungen) sind optimiert für schnelle Abfragen und Erweiterbarkeit.
- **Klassendesign**: Die Trennung von Rollen wie `Admin` und `User` unterstützt Wartbarkeit und Erweiterbarkeit.
- **Scrum-Setup**: Iterative Entwicklungszyklen mit Reviews und Retrospektiven fördern die kontinuierliche Verbesserung der Architektur.
