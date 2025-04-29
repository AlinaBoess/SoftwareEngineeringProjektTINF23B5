# Inhaltsverzeichnis

1. [Einleitung](#1-einleitung)  
    1.1 [Zweck](#11-zweck)  
    1.2 [Umfang](#12-umfang)  
    1.3 [Zielpublikum](#13-zielpublikum)  
    1.4 [Begriffserklärungen und Abkürzungen](#14-begriffserklärungen-und-abkürzungen)  
    1.5 [Referenzen](#15-referenzen)  
    1.6 [Dokumentenstruktur](#16-dokumentenstruktur)  
2. [Evaluierungsmission und Testmotivation](#2-evaluierungsmission-und-testmotivation)  
    2.1 [Hintergrund](#21-hintergrund)  
    2.2 [Evaluierungsmission](#22-evaluierungsmission)  
    2.3 [Testmotivatoren](#23-testmotivatoren)  
3. [Ziel-Testobjekte](#3-ziel-testobjekte)  
4. [Geplante Tests](#4-geplante-tests)  
    4.1 [Einzuschließende Tests](#41-einzuschließende-tests)  
    4.2 [Weitere potenzielle Tests](#42-weitere-potenzielle-tests)  
    4.3 [Ausgeschlossene Tests](#43-ausgeschlossene-tests)  
5. [Testansatz](#5-testansatz)  
    5.1 [Quellen der ersten Testideen](#51-quellen-der-ersten-testideen)  
    5.2 [Testtechniken und -arten](#52-testtechniken-und--arten)  
6. [Einstiegs- und Ausstiegskriterien](#6-einstiegs--und-ausstiegskriterien)  
7. [Liefergegenstände](#7-liefergegenstände)  
8. [Testablauf](#8-testablauf)  
9. [Umgebungsanforderungen](#9-umgebungsanforderungen)  
10. [Verantwortlichkeiten, Personal- und Schulungsbedarf](#10-verantwortlichkeiten-personal--und-schulungsbedarf)  
11. [Iteration-Meilensteine](#11-iteration-meilensteine)  
12. [Risiken, Abhängigkeiten, Annahmen und Einschränkungen](#12-risiken-abhängigkeiten-annahmen-und-einschränkungen)  
13. [Managementprozesse und -verfahren](#13-managementprozesse-und--verfahren)  

---

# 1. Einleitung

## 1.1 Zweck

Der Zweck dieses Iteration-Testplans ist es, alle notwendigen Informationen zu sammeln, um den Testaufwand für eine bestimmte Iteration zu planen und zu steuern.  
Dieses Dokument beschreibt den Ansatz für die Testung der Software und dient als oberster Plan, der von Testmanagern zur Steuerung des Testprozesses verwendet wird.

Dieser Testplan für das Projekt **Restaurantreservierung** verfolgt folgende Ziele:
- Identifikation der Elemente, die durch Tests abgedeckt werden sollen.
- Darstellung der Motivation und Ideen hinter den geplanten Testbereichen.
- Skizzierung des zu verwendenden Testansatzes.
- Definition der benötigten Ressourcen und Schätzung des Testaufwands.
- Auflistung der zu erstellenden und zu liefernden Testartefakte.

## 1.2 Umfang

Dieser Testplan umfasst die folgenden Testebenen:
- **Unit-Tests**: Überprüfung einzelner Komponenten oder Methoden isoliert voneinander.
- **Integrationstests**: Sicherstellung der korrekten Zusammenarbeit mehrerer Komponenten.
- **Systemtests**: Test der Anwendung als Gesamtsystem in einer produktionsähnlichen Umgebung.

Es werden folgende Testarten abgedeckt:
- **Funktionalitätstests**: Verifikation, dass die Anwendung die spezifizierten Anforderungen erfüllt.
- **Oberflächentests (UI-Tests)**: Prüfung der Benutzeroberfläche auf Funktionalität und Benutzerfreundlichkeit.
- **Zuverlässigkeitstests**: Tests, um die Stabilität und Fehlerresistenz unter normalen und außergewöhnlichen Bedingungen zu bewerten.
- **Leistungstests**: Messung der Antwortzeiten und des Durchsatzes unter verschiedenen Lastbedingungen.
- **Unterstützbarkeitstests**: Bewertung der Wartbarkeit, Konfigurierbarkeit und Erweiterbarkeit des Systems.

### Ausschlüsse aus dem Testumfang
- **Explizite Hardware-Tests** (z. B. spezifische Treiber oder Gerätekompatibilität) sind nicht Bestandteil dieses Testplans.
- **Manuelle ausführliche Usability-Studien** werden nicht innerhalb dieses Iterationsplans durchgeführt; leichte Usability-Aspekte werden jedoch im Rahmen der UI-Tests berücksichtigt.
- **Explorative Tests** außerhalb des definierten Testplans werden separat behandelt und sind nicht primärer Bestandteil dieser Teststrategie.

## 1.3 Zielpublikum

Dieses Dokument richtet sich an alle Projektbeteiligten, die an der Qualitätssicherung und der Abnahme des Produkts beteiligt sind. Insbesondere zählen dazu:

- **Testmanager und Testingenieure**, die für die Planung, Durchführung und Auswertung der Tests verantwortlich sind.
- **Entwickler**, die basierend auf den Testergebnissen Fehlerbehebungen und Optimierungen durchführen.
- **Projektmanager und Produktverantwortliche**, die einen Überblick über den Teststatus und die Testabdeckung benötigen.
- **Qualitätssicherungsbeauftragte**, die die Einhaltung von Qualitätsstandards und -richtlinien überwachen.

Das Dokument dient dazu, Transparenz über den geplanten Testansatz zu schaffen und sicherzustellen, dass alle Beteiligten ein gemeinsames Verständnis der Teststrategie, -ziele und -umfänge haben.  
Es ist nicht für Endnutzer oder externe Stakeholder bestimmt, die keine direkte Beteiligung am Testprozess haben.

## 1.4 Begriffserklärungen und Abkürzungen

In diesem Abschnitt werden Begriffe, Abkürzungen und Akronyme aufgeführt, die für das Verständnis des Testplans erforderlich sind:

| Begriff / Abkürzung | Bedeutung |
|---------------------|-----------|
| **Unit-Test** | Test einzelner Softwarekomponenten auf korrekte Funktion |
| **Integrationstest** | Test des Zusammenspiels mehrerer Komponenten oder Systeme |
| **Systemtest** | Gesamtheitliche Prüfung des vollständigen Systems |
| **UI** | User Interface (Benutzeroberfläche) |
| **NUnit** | Testframework für Unit-Tests in .NET |
| **Selenium** | Framework für die Automatisierung von Webbrowser-Interaktionen |
| **CI/CD** | Continuous Integration / Continuous Deployment |
| **Testabdeckung** | Maß für den Anteil des Codes, der durch Tests geprüft wird |


## 1.5 Referenzen

Die folgenden Quellen werden in diesem Testplan referenziert und bilden die Grundlage für Definitionen, Vorgehensweisen und Teststandards:

| Titel | Version | Datum | Autor / Organisation | Quelle |
|-------|---------|-------|----------------------|--------|
| NUnit-Dokumentation | Laufend | Zugriff: April 2025 | NUnit Project | [https://nunit.org](https://nunit.org) |
| Selenium WebDriver Dokumentation | Laufend | Zugriff: April 2025 | Selenium HQ | [https://www.selenium.dev/documentation/](https://www.selenium.dev/documentation/) |
| OWASP Testing Guide | v4.0 | 2021 | OWASP Foundation | [https://owasp.org/www-project-web-security-testing-guide/](https://owasp.org/www-project-web-security-testing-guide/) |
| ISO/IEC/IEEE 29119 Teststandards (Zusammenfassung) | n/a | Zugriff: April 2025 | Diverse Quellen | [https://en.wikipedia.org/wiki/ISO/IEC/IEEE_29119](https://en.wikipedia.org/wiki/ISO/IEC/IEEE_29119) |

Diese Referenzen dienen als fachliche Grundlage für die im Testplan beschriebenen Methoden, Tools und Qualitätsanforderungen.

## 1.6 Dokumentenstruktur

Dieses Dokument ist strukturiert, um den gesamten Lebenszyklus der Testplanung übersichtlich darzustellen. Es beginnt mit einleitenden Informationen zu Zweck, Umfang und Zielpublikum (Kapitel 1), bevor es in die Details der Teststrategie, der zu testenden Komponenten und des geplanten Vorgehens übergeht (Kapitel 2 bis 6). 

Die folgenden Abschnitte beschäftigen sich mit organisatorischen und technischen Rahmenbedingungen, wie Liefergegenständen, Testumgebungen, Personalressourcen und Zeitplänen (Kapitel 7 bis 11). Risiken, Abhängigkeiten und Managementprozesse schließen den Testplan ab (Kapitel 12 und 13).

Durch das **ausführliche Inhaltsverzeichnis** am Anfang ist eine gezielte Navigation möglich. Jeder Abschnitt kann unabhängig gelesen werden, verweist jedoch sinnvoll auf verwandte Kapitel für den kontextuellen Zusammenhang.


## 2. Evaluation Mission und Testmotivation

Dieses Kapitel beschreibt den übergeordneten Zweck und die Beweggründe für die Testaktivitäten innerhalb der aktuellen Iteration. Es liefert den kontextuellen Rahmen, innerhalb dessen Testentscheidungen getroffen und priorisiert werden. Ziel ist es, die Beweggründe für das Testen sowie die strategische Ausrichtung der Testmaßnahmen nachvollziehbar darzustellen.

### 2.1 Hintergrund

Das aktuelle Projekt befindet sich in der Entwicklung einer webbasierten Anwendung, die eine spezifische geschäftskritische Funktionalität abbildet. Diese Anwendung wird in einem .NET-Umfeld entwickelt und nutzt moderne Webtechnologien im Frontend. Das Projekt befindet sich derzeit in der Phase funktionaler Fertigstellung, in der die Kernkomponenten bereitstehen, jedoch noch intensiv getestet und validiert werden müssen.

Der Hauptnutzen der Lösung liegt in der Automatisierung und Vereinfachung bestehender, manuell durchgeführter Prozesse. Zu diesem Zweck wurde eine mehrschichtige Systemarchitektur implementiert, bestehend aus Datenbank, Business-Logik, API-Services und einer webbasierten Benutzeroberfläche.

In den bisherigen Entwicklungszyklen wurden erste Unit-Tests mit NUnit implementiert. Die nächste Phase erfordert jedoch eine umfassendere Teststrategie, die neben Komponenten- und Integrationstests auch UI-basierte Tests mit Selenium sowie nicht-funktionale Tests (z. B. Sicherheit und Stabilität) berücksichtigt.

Das Testvorhaben ist essenziell, um sicherzustellen, dass das System nicht nur funktional korrekt arbeitet, sondern auch unter realistischen Nutzungsbedingungen stabil, sicher und benutzerfreundlich ist.


### 2.2 Testmission

Die Testmission dieser Iteration besteht darin:

- **möglichst viele funktionale und technische Fehler frühzeitig zu identifizieren**,
- **wesentliche Risiken im Hinblick auf Qualität und Projektverzögerung zu erkennen und zu kommunizieren**,
- **die Einhaltung von funktionalen Anforderungen zu verifizieren**, sowie
- **Stakeholder über den aktuellen Qualitätsstand zu informieren**.

Besonderes Augenmerk liegt auf der Integration von automatisierten Tests, um Regressionen frühzeitig zu erkennen, sowie auf der Durchführung von UI-Tests mit Selenium zur Sicherstellung der Nutzerinteraktion. Damit soll eine stabile Grundlage für ein sicheres Deployment geschaffen werden.

Die Tests dienen nicht nur der Fehlererkennung, sondern auch der kontinuierlichen Verbesserung der Softwarequalität und der Einhaltung projektinterner Standards.


### 2.3 Testmotivatoren

Die Testaktivitäten in dieser Iteration werden durch mehrere zentrale Faktoren motiviert:

- **Qualitätsrisiken**: Die Applikation wird produktiv in einem sicherheitsrelevanten Kontext eingesetzt, weshalb Fehler schwerwiegende Konsequenzen haben können.
- **Technische Risiken**: Es gibt komplexe Schnittstellen zwischen Backend-Services und der Benutzeroberfläche, die fehleranfällig sind.
- **Funktionale Anforderungen**: Die definierten Use Cases müssen vollständig und korrekt umgesetzt sein.
- **Nicht-funktionale Anforderungen**: Es bestehen Anforderungen an Performance, Sicherheit und Usability, die erfüllt werden müssen.
- **Regressionen**: Durch zahlreiche Code-Änderungen besteht ein erhöhtes Risiko von Nebenwirkungen, das durch automatisierte Tests minimiert werden soll.
- **Change Requests**: Neue oder geänderte Anforderungen müssen gezielt nachgetestet werden, um unbeabsichtigte Auswirkungen zu erkennen.

Die Kombination dieser Motivatoren gibt der Teststrategie ihre Richtung und beeinflusst sowohl die Auswahl der Testarten als auch deren Priorisierung.

## 3. Zu testende Komponenten (Target Test Items)

In diesem Abschnitt werden alle relevanten Elemente aufgeführt, die im Rahmen der aktuellen Testiteration überprüft werden sollen. Dabei handelt es sich sowohl um Eigenentwicklungen des Projektteams als auch um Fremdkomponenten oder unterstützende Systembestandteile, auf die das Produkt angewiesen ist.

Die Auflistung ist thematisch gegliedert und enthält jeweils eine Einschätzung der Testrelevanz.

### 3.1 Softwarekomponenten (Eigenentwicklung)

| Komponente                     | Beschreibung                                                                 | Testrelevanz |
|-------------------------------|------------------------------------------------------------------------------|--------------|
| **Backend-API (.NET)**        | Business-Logik, Schnittstellen zu Datenbank und Frontend                    | Hoch         |
| **Datenbank (SQL Server)**    | Persistenzschicht, enthält strukturierte Datenmodelle und Logik             | Hoch         |
| **Web-Frontend (Blazor)**     | Benutzeroberfläche, durch Benutzer direkt bedient                           | Hoch         |
| **Authentifizierungsmodul**   | Nutzeranmeldung, Rollen- und Rechteverwaltung                               | Hoch         |


### 3.2 Infrastruktur und Third-Party-Komponenten

| Komponente                     | Beschreibung                                                                 | Testrelevanz |
|-------------------------------|------------------------------------------------------------------------------|--------------|
| **Betriebssystem (Windows)**  | Host-System der Anwendung im Deployment                                     | Mittel       |
| **Drittanbieter-UI-Komponenten** | Eingesetzte Bibliotheken für UI-Funktionalitäten (z. B. Syncfusion)         | Mittel       |
| **NUnit-Test-Framework**      | Framework zur Ausführung automatisierter Unit- und UI-Tests                 | Mittel       |
| **Selenium WebDriver**        | Tool für automatisierte End-to-End-Tests im Browser                         | Hoch         |


### 3.3 Unterstützende Tools und Dienste

| Komponente                     | Beschreibung                                                                 | Testrelevanz |
|-------------------------------|------------------------------------------------------------------------------|--------------|
| **Build- und CI-Pipeline**    | Automatisierter Build, Test und Deployment (z. B. GitHub Actions)           | Mittel       |
| **Logging-System**            | Diagnostik und Fehlererkennung                                              | Mittel       |
| **Monitoring-Tooling (optional)** | Überwachung von Performance und Verfügbarkeit im Betrieb                  | Niedrig      |


Die oben aufgeführten Testobjekte sind essenziell für die Gesamtfunktionalität und -sicherheit der Anwendung. Der Fokus liegt auf der funktionalen Korrektheit, Integration zwischen den Schichten sowie der Benutzerinteraktion über das Web-Frontend.

## 4. Übersicht der geplanten Tests (Outline of Planned Tests)

In diesem Kapitel wird ein Überblick über die für diese Iteration geplanten Testaktivitäten gegeben. Ziel ist es, sowohl die konkreten Testumfänge als auch explizit ausgeschlossene Tests zu dokumentieren. Zudem werden potenzielle Testbereiche aufgeführt, deren Relevanz aktuell noch nicht abschließend beurteilt werden kann.

### 4.1 Geplante Testinhalte (Outline of Test Inclusions)

Folgende Testarten und Testebenen sind für diese Iteration geplant und werden aktiv umgesetzt:

- **Unit-Tests (NUnit)**  
  Automatisierte Tests zur Überprüfung einzelner Methoden und Funktionen im Backend. Fokus liegt auf Logik, Validierung und kleineren Funktionseinheiten.

- **Integrationstests**  
  Tests von Schnittstellen zwischen Komponenten wie API, Datenbank und Authentifizierung. Fokus auf Datenflüsse und Fehlerbehandlung.

- **Oberflächentests / UI-Tests (NUnit + Selenium)**  
  Automatisierte Tests der Web-Oberfläche im Browser (z. B. Benutzeranmeldung, Navigation, Formularvalidierung). Durchführung mit Selenium WebDriver.

- **Regressions-Tests**  
  Wiederholte Durchführung bestehender Testfälle nach Änderungen, um sicherzustellen, dass keine Nebenwirkungen entstehen.

- **Smoke Tests (Build-Verifikation)**  
  Schnelle Tests nach jedem Deployment zur Sicherstellung, dass das System grundsätzlich lauffähig ist.

- **Manuelle explorative Tests**  
  Ergänzend zu automatisierten Tests: händische Tests, insbesondere im Bereich der Usability und bei komplexen Benutzerinteraktionen.

### 4.2 Weitere potenzielle Testkandidaten (Outline of Other Candidates for Potential Inclusion)

Folgende Testbereiche werden derzeit als potenziell sinnvoll betrachtet, wurden jedoch noch nicht vollständig bewertet oder vorbereitet:

- **Last- und Performancetests**  
  Erste Überlegungen bestehen, jedoch fehlen bisher definierte Metriken, Tools und Zielwerte. Eignung muss noch analysiert werden.

- **Barrierefreiheitstests (Accessibility)**  
  Grundsätzliche Unterstützung vorgesehen, aber keine konkreten Testpläne oder Anforderungen formuliert.

- **Mobile-Browser-Kompatibilität**  
  Die Webanwendung soll responsiv sein, aber systematische Tests auf mobilen Geräten sind bisher nicht eingeplant.

### 4.3 Ausgeschlossene Testbereiche (Outline of Test Exclusions)

Die folgenden Testarten werden bewusst **nicht** durchgeführt, entweder aus Ressourcengründen oder weil sie für das Projekt als nicht zielführend bewertet wurden:

- **Hardwarekompatibilitätstests**  
  > Diese Tests werden nicht durchgeführt, da die Anwendung rein webbasiert ist und keine direkte Interaktion mit spezieller Hardware erfordert.

- **Failover- und Recovery-Tests**  
  > Nicht implementiert, da Hochverfügbarkeit und Redundanz aktuell nicht Bestandteil der Systemarchitektur sind.

- **Penetrationstests durch externe Dienstleister**  
  > Derzeit nicht vorgesehen aufgrund fehlender Budgetierung und niedriger priorisierter Sicherheitsanforderungen.

- **Installationstests**  
  > Nicht notwendig, da es sich um eine Webanwendung handelt, bei der die Installation zentral im Server-Deployment erfolgt.


## 5. Testansatz (Test Approach)

In diesem Kapitel wird die Strategie zur Durchführung der geplanten Tests beschrieben. Aufbauend auf den vorherigen Kapiteln zu Testgegenständen und Testumfang wird hier erläutert, **wie** die Tests umgesetzt werden. Dabei werden sowohl manuelle als auch automatisierte Techniken berücksichtigt. Zudem wird erläutert, welche Tools, Erfolgskriterien und Prüfmethoden (Orakel) dabei zum Einsatz kommen. 

### 5.1 Erste Testideen-Kataloge und Referenzquellen

Für die Identifikation und Auswahl konkreter Testfälle werden folgende Quellen herangezogen:

- **Use Cases und User Stories** aus dem Produkt-Backlog
- **Nicht-funktionale Anforderungen** (z. B. aus Sicherheits-, Performance- oder Usability-Spezifikationen)
- **Bugreports und Change Requests** aus vorherigen Releases
- **Erfahrung aus ähnlichen Projekten** (Heuristiken)
- **Testideen-Kataloge** aus Fachliteratur (z. B. „Exploratory Testing Heuristics“ oder „Test Design Patterns“)

### 5.2 Testtechniken und -arten

#### 5.2.1 Test der Daten- und Datenbankintegrität

**Ziel:**  
Datenbankprozesse unabhängig vom UI testen, um Datenintegrität und korrektes Verhalten sicherzustellen.

**Technik:**  
- Aufruf aller Datenbankfunktionen mit gültigen und ungültigen Eingaben
- Überprüfung der Datenbank auf korrekte Speicherung oder Rückgabe der Daten

**Orakel:**  
- Direkter Datenbankvergleich (SQL-Assertions)
- Validierung gegen Erwartungswerte
- Logauswertung von DB-Ereignissen

**Benötigte Werkzeuge:**  
- SQL-Clients und -Skripte  
- Datenbank-Monitoring-Tools  
- Testautomatisierung (z. B. NUnit + SQLUnit)  
- Daten-Generatoren und Backup-/Recovery-Tools

**Erfolgskriterien:**  
Alle zentralen Datenbankoperationen werden korrekt ausgeführt und fehlerhaftes Verhalten wird erkannt.

**Besonderheiten:**  
- Manuelle Eingriffe ggf. notwendig  
- Kleinere Testdatenbanken zur besseren Nachverfolgbarkeit  
- Zugriff auf DBMS-Entwicklungsumgebungen nötig

#### 5.2.2 Funktionstest

**Ziel:**  
Prüfen von Use Cases, Geschäftsregeln und Funktionen über die UI anhand gültiger und ungültiger Daten.

**Technik:**  
- Ausführung von Funktionsflüssen gemäß Use Cases  
- Überprüfung auf korrekte Resultate, Fehler- und Warnmeldungen

**Orakel:**  
- Erwartungswerte aus Anforderungen  
- Visuelle Prüfung der Resultate (manuell oder per Screenshot-Vergleich)  
- Validierung durch automatisierte UI-Tests (Selenium)

**Benötigte Werkzeuge:**  
- Selenium + NUnit  
- Daten-Generatoren  
- Testdaten-Vorlagen  
- UI-Testskripte

**Erfolgskriterien:**  
Alle kritischen Use Cases und Geschäftsregeln funktionieren wie spezifiziert.

**Besonderheiten:**  
- Gute Testdatenabdeckung notwendig  
- Fehlermeldungen müssen klar, eindeutig und verständlich sein

#### 5.2.3 Geschäftszyklustests

**Ziel:**  
Simulation typischer Geschäftsprozesse über längere Zeiträume (z. B. 1 Jahr), inkl. wiederkehrender Ereignisse.

**Technik:**  
- Wiederholte Ausführung von Funktionstests unter Zeitbedingungen  
- Simulation von periodischen Abläufen (z. B. Monatswechsel, Jahresabschlüsse)  
- Test mit zeitabhängigen Daten (z. B. Gültigkeitszeiträume, Ablauffristen)

**Orakel:**  
- Validierte Ergebnisdaten  
- Zeitliche Konsistenzprüfungen  
- Abgleich mit Geschäftslogik

**Benötigte Werkzeuge:**  
- Zeitsteuerungstools (z. B. Cron-Simulation)  
- Automatisierte Testumgebungen  
- Datenaufbereitungstools

**Erfolgskriterien:**  
Alle relevanten Zyklen (täglich, monatlich, jährlich) können fehlerfrei durchlaufen werden.

**Besonderheiten:**  
- Datumssprünge müssen gezielt testbar gemacht werden  
- Modell des typischen Geschäftsverlaufs ist Voraussetzung

#### 5.2.4 Benutzeroberflächentest (UI-Test)

**Ziel:**  
Prüfung der UI auf korrekte Navigation, Interaktion und Einhaltung von Designstandards.

**Technik:**  
- Tests aller Fenster und Interaktionen (z. B. Tab-Reihenfolge, Buttons, Navigation)  
- Überprüfung von Objektzuständen (z. B. Sichtbarkeit, Aktivität, Position)

**Orakel:**  
- Sichtprüfung durch Testpersonen  
- Screenshot-Vergleiche mit Referenzzuständen  
- Automatisierte UI-Tests (Selenium/WebDriver)

**Benötigte Werkzeuge:**  
- Selenium  
- UI-Automation-Frameworks  
- Screen-Capture-Tools

**Erfolgskriterien:**  
Jede relevante Oberfläche ist erreichbar, vollständig und funktioniert wie erwartet.

**Besonderheiten:**  
- Einige Custom-Controls erfordern individuelle Prüfmethoden  
- Detaillierte UI-Standards erforderlich


### 5.2.5 Performance-Profiling

Performance-Profiling ist ein Leistungstest, bei dem Antwortzeiten, Transaktionsraten und andere zeitkritische Anforderungen gemessen und bewertet werden. Das Ziel des Performance-Profilings besteht darin, sicherzustellen, dass die Leistungsanforderungen erfüllt werden. Es wird implementiert und durchgeführt, um das Performance-Verhalten des Testobjekts in Abhängigkeit von Bedingungen wie Arbeitslast oder Hardware-Konfigurationen zu profilieren und zu optimieren.

**Hinweis:** In der folgenden Tabelle beziehen sich Transaktionen auf „logische Geschäftstransaktionen“. Diese Transaktionen werden als spezifische Anwendungsfälle definiert, die ein Benutzer des Systems voraussichtlich mit dem Testobjekt ausführt, wie z. B. das Hinzufügen oder Ändern eines Vertrags.

**Technikziel:**  
Verhalten für definierte funktionale Transaktionen oder Geschäftsprozesse unter den folgenden Bedingungen ausüben, um das Zielverhalten und Anwendungsleistungsdaten zu beobachten und aufzuzeichnen:  
- Normale, erwartete Arbeitslast  
- Erwartete Worst-Case-Arbeitslast  

**Technik:**  
- Verwenden Sie Testverfahren, die für die Funktions- oder Geschäftszyklen-Tests entwickelt wurden.  
- Ändern Sie die Datendateien, um die Anzahl der Transaktionen zu erhöhen oder die Skripte so zu modifizieren, dass die Anzahl der Iterationen pro Transaktion erhöht wird.  
- Skripte sollten auf einem einzelnen Rechner ausgeführt werden (bestes Szenario zur Benchmarking eines einzelnen Benutzers mit einer Transaktion) und wiederholt mit mehreren Clients (virtuellen oder tatsächlichen) durchgeführt werden.  

**Orakel:**  
Um ein genaues Ergebnis des Tests zu beobachten, können verschiedene Strategien genutzt werden. Ein Orakel kombiniert Methoden der Beobachtung und Merkmale eines spezifischen Ergebnisses, das den Test als erfolgreich oder fehlgeschlagen kennzeichnet. Idealerweise sind Orakel selbstverifizierend, sodass automatisierte Tests eine erste Bewertung des Testergebnisses vornehmen können. Es muss jedoch darauf geachtet werden, die Risiken zu minimieren, die mit der automatisierten Bestimmung des Testergebnisses verbunden sind.

**Erforderliche Werkzeuge:**  
- Testskript-Automatisierungstool  
- Ein Anwendungs-Performance-Profiling-Tool wie Rational Quantify  
- Überwachungswerkzeuge zur Installation (z. B. für das Registrierungs-, Festplatten-, CPU- und Speicher-Management)  
- Ressourcenbegrenzungstools, z. B. Canned Heat  

**Erfolgskriterien:**  
Der Test unterstützt das Testen von:  
- Einer einzelnen Transaktion oder einem einzelnen Benutzer: Erfolgreiche Emulation des Transaktionsskripts ohne Fehler durch Testimplementierungsprobleme.  
- Mehrere Transaktionen oder mehrere Benutzer: Erfolgreiche Emulation der Arbeitslast ohne Fehler durch Testimplementierungsprobleme.

**Besondere Überlegungen:**  
Ein umfassendes Performance-Testing schließt ein Hintergrund-Workload auf dem Server mit ein. Es gibt verschiedene Methoden, um dies zu erreichen, z. B.:  
- „Transaktionen“ direkt an den Server senden, meistens in Form von Structured Query Language (SQL)-Anrufen.  
- „Virtuelle“ Benutzerlast erstellen, um viele Clients zu simulieren, in der Regel mehrere Hundert. Dies wird üblicherweise mit Remote-Terminal-Emulationswerkzeugen durchgeführt. Diese Technik kann auch genutzt werden, um das Netzwerk mit „Verkehr“ zu belasten.  
- Verwendung mehrerer physischer Clients, die jeweils Testskripte ausführen, um eine Last auf das System zu erzeugen.

Performance-Tests sollten auf einem dedizierten Rechner oder zu einer festgelegten Zeit durchgeführt werden, um vollständige Kontrolle und genaue Messungen zu gewährleisten. Die für Performance-Tests verwendeten Datenbanken sollten entweder die tatsächliche Größe oder eine gleichwertige Skalierung aufweisen.


### 5.2.6 Lasttest

Ein Lasttest ist ein Leistungstest, bei dem das Testobjekt unterschiedlichen Arbeitslasten ausgesetzt wird, um die Leistungsmerkmale und Fähigkeiten des Systems zu messen und zu bewerten, damit es auch unter diesen unterschiedlichen Arbeitslasten ordnungsgemäß funktioniert. Das Ziel des Lasttests ist es, sicherzustellen, dass das System auch über die erwartete maximale Arbeitslast hinaus korrekt funktioniert. Außerdem bewertet der Lasttest die Leistungsmerkmale, wie Antwortzeiten, Transaktionsraten und andere zeitkritische Aspekte.

**Technikziel:**  
Führen Sie definierte Transaktionen oder Geschäftsprozesse unter variierenden Arbeitslastbedingungen aus, um das Zielverhalten und die Systemleistung zu beobachten und aufzuzeichnen.

**Technik:**  
- Verwenden Sie Transaktions-Testskripte, die für Funktions- oder Geschäftszyklen-Tests entwickelt wurden, jedoch ohne unnötige Interaktionen und Verzögerungen.  
- Ändern Sie die Datendateien, um die Anzahl der Transaktionen zu erhöhen oder die Tests so zu modifizieren, dass jede Transaktion mehrfach ausgeführt wird.  
- Arbeitslasten sollten z. B. tägliche, wöchentliche und monatliche Spitzenlasten umfassen.  
- Arbeitslasten sollten sowohl Durchschnitts- als auch Spitzenlasten darstellen.  
- Arbeitslasten sollten sowohl sofortige als auch anhaltende Spitzen umfassen.  
- Die Arbeitslasten sollten unter verschiedenen Testumgebungs-Konfigurationen ausgeführt werden.

**Orakel:**  
Das Orakel kombiniert Elemente der Methode der Beobachtung und Merkmale spezifischer Ergebnisse, die eine wahrscheinliche Bestimmung des Erfolgs oder Misserfolgs angeben. Es ist ideal, wenn Orakel selbstverifizierend sind, sodass automatisierte Tests eine erste Bewertung des Testergebnisses durchführen können, allerdings müssen Risiken, die mit der automatisierten Bestimmung von Testergebnissen verbunden sind, berücksichtigt werden.

**Erforderliche Werkzeuge:**  
- Testskript-Automatisierungstool  
- Transaktionslast-Steuerungs- und Planungswerkzeug  
- Überwachungswerkzeuge zur Installation (z. B. Festplatten-, CPU-, Speicher-Management)  
- Ressourcenbegrenzungstools (z. B. Canned Heat)  
- Daten-Generierungswerkzeuge  

**Erfolgskriterien:**  
Der Test unterstützt das Testen von Arbeitslast-Emulation, wobei die erfolgreiche Emulation der Arbeitslast ohne Fehler durch Testimplementierungsprobleme sichergestellt wird.

**Besondere Überlegungen:**  
- Lasttests sollten auf einem dedizierten Rechner oder zu einem dedizierten Zeitpunkt durchgeführt werden, um vollständige Kontrolle und genaue Messungen zu gewährleisten.  
- Die für Lasttests verwendeten Datenbanken sollten entweder die tatsächliche Größe oder eine gleichwertige Skalierung aufweisen.


### 5.2.7 Stresstest

Der Stresstest ist eine spezielle Art von Leistungstest, bei dem das System mit extrem hohen oder unvorhergesehenen Belastungen konfrontiert wird, um zu überprüfen, wie es auf ungewöhnliche Bedingungen reagiert. Das Ziel des Stresstests ist es, die Robustheit und Stabilität des Systems unter extremen Bedingungen zu prüfen und zu überprüfen, wie es sich bei einem Überschreiten der maximalen Kapazität verhält. Dies kann dazu beitragen, Schwachstellen im System zu identifizieren, die bei normalen Betriebsbedingungen möglicherweise nicht erkennbar sind.

**Technikziel:**  
Prüfung der maximalen Belastungsgrenze des Systems, um sicherzustellen, dass es unter extremen Bedingungen entweder stabil bleibt oder erwartungsgemäß auf eine Fehlerbehandlung umschaltet.

**Technik:**  
- Der Test simuliert Bedingungen, die das System an seine Kapazitätsgrenzen bringen. Dies kann durch die Erhöhung der Anzahl der gleichzeitigen Benutzer, Transaktionen oder Anfragen über die festgelegten Leistungsgrenzen hinaus erfolgen.  
- Der Stresstest kann auch das Verharren in einem Zustand der extremen Belastung umfassen, um zu prüfen, ob das System reagiert und wiederhergestellt werden kann.  
- Ein plötzlicher und unerwarteter Anstieg der Arbeitslast wird simuliert, um die Fähigkeit des Systems zu testen, mit plötzlichen Veränderungen oder Ausfällen umzugehen.

**Orakel:**  
Das Orakel sollte definierte Ergebnisse wie Systemabstürze, Leistungseinbußen oder Zeitüberschreitungen überwachen. Es hilft, die Belastbarkeit des Systems zu bewerten und mögliche Fehlerquellen zu identifizieren.

**Erforderliche Werkzeuge:**  
- Testskript-Automatisierungstool  
- Lastgenerierungswerkzeuge (z. B. Apache JMeter oder LoadRunner)  
- Ressourcenüberwachungswerkzeuge zur Messung von CPU-, Speicher- und Festplattenauslastung  
- Logging-Tools zur Aufzeichnung von Systemereignissen und Fehlern  

**Erfolgskriterien:**  
- Das System sollte unter Stress entweder in den Wartungsmodus gehen, einen Fehlerbehandlungsmechanismus auslösen oder bestimmte Schwellenwerte überschreiten, ohne vollständig zu versagen.  
- Es sollten geeignete Warnmeldungen und Logs erzeugt werden, die die Überwachung und Nachverfolgung von Fehlern ermöglichen.

**Besondere Überlegungen:**  
- Der Stresstest sollte so konzipiert werden, dass er keine dauerhaften Schäden am System verursacht, aber dennoch eine realistische Belastung darstellt.  
- Die Tests müssen so gestaltet werden, dass sie unterschiedliche Fehlerbedingungen und Systemreaktionen wie Systemabstürze, Netzwerkprobleme oder Festplattenfehler simulieren.


### 5.2.8 Skalierbarkeitstest

Der Skalierbarkeitstest bewertet, wie gut das System mit einer zunehmenden Belastung oder einer Erhöhung der Systemressourcen umgehen kann. Es wird getestet, ob das System in der Lage ist, zusätzliche Anforderungen effizient zu verarbeiten, ohne dass es zu einer signifikanten Verschlechterung der Leistung kommt. Dieser Test hilft zu überprüfen, ob das System in der Lage ist, mit einer wachsenden Benutzerzahl oder steigenden Datenmengen mitzuhalten.

**Technikziel:**  
Prüfung, wie das System auf eine Zunahme der Belastung reagiert, und Überprüfung, ob es bei Skalierung weiterhin effizient und stabil bleibt.

**Technik:**  
- Erhöhung der Anzahl von Transaktionen, Anfragen oder Benutzern und Messung der Auswirkungen auf die Systemleistung.  
- Die Skalierbarkeit kann sowohl horizontal (Hinzufügen zusätzlicher Maschinen oder Instanzen) als auch vertikal (Erhöhung der Kapazität eines einzelnen Systems durch mehr Ressourcen wie CPU, RAM oder Festplattenspeicher) getestet werden.  
- Messen der Leistungsverschlechterung und der maximalen Systemkapazität in Bezug auf verschiedene Parameter wie Antwortzeiten, Verarbeitungsgeschwindigkeit und Transaktionsrate.

**Orakel:**  
Das Orakel für den Skalierbarkeitstest umfasst Leistungskennzahlen, die sicherstellen, dass das System bei Skalierung weiterhin die akzeptablen Antwortzeiten und Transaktionsraten beibehält. Wenn eine signifikante Leistungsverschlechterung oder Instabilität auftritt, sollte dies als Fehler gewertet werden.

**Erforderliche Werkzeuge:**  
- Testskript-Automatisierungstool  
- Lastgenerierungs- und Überwachungstools  
- Ressourcen-Management-Tools zur Messung der Nutzung von CPU, Arbeitsspeicher und Netzwerkkapazität  
- Skalierungs-Tools zur Durchführung der Tests bei der horizontalen oder vertikalen Skalierung des Systems

**Erfolgskriterien:**  
- Das System sollte in der Lage sein, mit zunehmender Belastung ohne erhebliche Leistungsverschlechterung zu skalieren.  
- Es sollte eine gewisse Steigerung der Kapazität ohne signifikante Fehlfunktionen oder Systemabstürze erfolgen.

**Besondere Überlegungen:**  
- Der Test sollte für verschiedene Szenarien durchgeführt werden, in denen unterschiedliche Arten von Skalierung angewendet werden. Dies hilft, die besten Skalierungsstrategien für die spezifischen Anforderungen des Systems zu ermitteln.  
- Der Test sollte auch unter realistischen Bedingungen durchgeführt werden, die den tatsächlichen Einsatz des Systems widerspiegeln.



### 5.2.9 Sicherheitstests

Sicherheitstests überprüfen die Fähigkeit eines Systems, gegen Angriffe und unbefugte Zugriffe geschützt zu bleiben. Dabei wird geprüft, ob das System Schwachstellen aufweist, die potenziell ausgenutzt werden könnten, und ob Sicherheitsfunktionen wie Authentifizierung, Autorisierung und Verschlüsselung ordnungsgemäß funktionieren.

**Technikziel:**  
Sicherstellung, dass das System gegen gängige Sicherheitsbedrohungen wie unbefugten Zugriff, Datenlecks oder Missbrauch von Systemfunktionen geschützt ist.

**Technik:**  
- Durchführung von Penetrationstests, bei denen die Sicherheitslücken im System absichtlich ausgenutzt werden.  
- Testen von Authentifizierungsmechanismen, um sicherzustellen, dass nur autorisierte Benutzer auf die Ressourcen zugreifen können.  
- Überprüfung von Verschlüsselungstechnologien, um zu garantieren, dass Daten sicher übermittelt und gespeichert werden.  
- Durchführung von Sicherheitsscans und Tests, um bekannte Sicherheitslücken oder Schwachstellen zu identifizieren.

**Orakel:**  
Die Sicherheits-Tests sollten auf bekannte Schwachstellen und Bedrohungen basieren. Wenn das System gegen diese Bedrohungen nicht geschützt ist, gilt der Test als fehlgeschlagen.

**Erforderliche Werkzeuge:**  
- Penetrationstests-Tools wie Burp Suite oder Nessus  
- Sicherheitsüberwachungs- und Scanning-Tools  
- Authentifizierungs- und Verschlüsselungstests  

**Erfolgskriterien:**  
- Das System sollte nicht anfällig für bekannte Angriffe sein und alle Sicherheitsfunktionen sollten ordnungsgemäß funktionieren.  
- Alle Daten, die übertragen oder gespeichert werden, sollten sicher sein.

**Besondere Überlegungen:**  
- Es ist wichtig, dass Sicherheitslücken regelmäßig getestet werden, da neue Bedrohungen ständig auftreten.  
- Sicherheitstests sollten regelmäßig in den Testprozess integriert werden, um die langfristige Integrität des Systems zu gewährleisten.



