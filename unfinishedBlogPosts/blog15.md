## Blog #15  
Herzlich Willkommen bei unserem Blog – schön, dass ihr wieder hier seid!

---

### Metriken

In dieser Woche haben wir uns mit dem Thema Software-Metriken beschäftigt und drei konkrete Metriken ausgewählt, die wir in unserem Projekt einsetzen:

1. **Anzahl der HTTP-Requests pro Endpoint (Request Rate)**  
   Diese Metrik zeigt, wie oft bestimmte API-Endpunkte aufgerufen werden. Damit lassen sich besonders stark frequentierte Bereiche der Anwendung identifizieren.

2. **Antwortzeit (Request Duration / Response Time)**  
   Diese Metrik misst die Zeit, die die Anwendung benötigt, um eine Anfrage zu verarbeiten und eine Antwort zurückzugeben. Sie ist besonders relevant zur Bewertung der Performance.

3. **Anzahl aktiver Verbindungen (Active Requests/Connections)**  
   Gibt Auskunft darüber, wie viele Anfragen gleichzeitig bearbeitet werden. Diese Information hilft dabei, mögliche Engpässe in der Lastverteilung zu erkennen.

Zur Erfassung dieser Metriken verwenden wir **Prometheus** in Verbindung mit unserer **ASP.NET Core Web API**. Die Integration erfolgt über die Middleware des Pakets `prometheus-net.AspNetCore`. Nach dem Start der Anwendung sind die Metriken unter `https://localhost:7038/metrics` abrufbar.  

Prometheus ist ein leistungsstarkes Tool, das eine Vielzahl an Metriken zur Verfügung stellt – die oben genannten sind nur ein Ausschnitt dessen, was damit möglich ist. Auch Standardmetriken wie Speicherverbrauch, Garbage Collection oder System-Load sollen sich darüber beobachten lassen können.

---

### Sonstiges


Neben der Arbeit an den Metriken haben wir außerdem noch einige Aufgaben im **Frontend** und **Backend** in Bearbeitung. Hier sind wir aktuell noch dabei, einzelne Funktionen zu erweitern und zu verbessern.

---

Wir freuen uns darauf, in den kommenden Wochen weitere Einblicke zu gewinnen und die Metriken zur Optimierung unserer Anwendung zu nutzen.

Bis zum nächsten Eintrag!

Liebe Grüße  
Alina, Lukas, Alex, Moumen und Yahya
