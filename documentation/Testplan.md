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
- **Nicht-funktionale Anforderungen** (z.B. aus Sicherheits-, Performance- oder Usability-Spezifikationen)
- **Bugreports und Change Requests** aus vorherigen Releases
- **Erfahrung aus ähnlichen Projekten** (Heuristiken)
- **Testideen-Kataloge** aus Fachliteratur (z.B. „Exploratory Testing Heuristics“ oder „Test Design Patterns“)

### 5.2 Testtechniken und -arten

#### 5.1.1 Unit Tests

Unit testing ensures, that the tested sourcecode works as expected. Therefore small parts of the sourcecode are tested independently.

|                       | Beschreibung                                                         |
|-----------------------|---------------------------------------------------------------------|
|Zielsetzung    | Sicherstellen, dass der implmentierte Quellcode wie erwartet funktioniert.                 |
|Strategie              | Test-Klassen implementieren mit dem NUnit-Framework für C# ASP.NET   |
|Benötigte Tools         | NUnit 4.3                   |
|Erfolgskriterien       | Alle Tests werden erfolgreich ausgeführt. Die Testabdeckung liegt bei mindestens 70% für Backend-Quellcode.   |

#### 5.1.2 UI-Tests

UI-Tests dienen zur Validierung der Anwendung aus der Benutzerperspektive, womit das Ziel verfolgt wird, sicherzustellen, dass alle für Benutzer angebotenen Funktionalitäten korrekt funktionieren.

|                       | Beschreibung                                                          |
|-----------------------|----------------------------------------------------------------------|
|Zielsetzung        | Automatisiert ausführbare UI-Tests mit Selenium. |
|Strategie              | Erstellen von Testanweisungen durch das Erzeugen dedizierter .cs-Dateien unter der Verwendung der Selenium-Schnittstellen. |
|Benötigte Tools        | NUnit, MvcTesting  |
|Erfolgskriterien       | Alle UI-Tests werden erfolgreich ausgeführt. 

#### 5.1.3 Integrations-Tests (API Tests)

Ein zentraler Aspekt von Integrationstests stellt das Überprüfen zentraler APIs der Anwendung dar.
Dabei werden mehrere Teilsysteme einer Lösung kombiniert getestet, wodurch sich komplexere Abläufe leicht simulieren lassen, ohne auf extensive Mocking-Strategien zurückfallen zu müssen.

|                       | Beschreibung                                                          |
|-----------------------|----------------------------------------------------------------------|
|Zielsetzung    | Automatisiert ausführbare Integrationstests mit NUnit.                                |
|Strategie              | Für jedes größere, für Basisfunktionalitäten des Reservierungssystems verantwortliche Prozesse (Starten des Backends, Erstellen von Reservierungen, ...) werden Integrationstests mit NUnit deklariert. |            |
|Benötigte Tools         | NUnit                                   |
|Erfolgskriterien       | Alle Integrationstests werden erfolgreich ausgeführt, wobei alle obig definierten Kernfunktionen suffizient abgedeckt sind.                               |
|Besondere Kriterien   | Zur Vermeidung von residualen Elementen in der Produktivdatenbank nach dem Ausführen von Tests wird für diesen Testschritt eine In-Memory Datenbanklösung verwendet.

## 6. Start- und Finalbedingungen 

### 6.1 Test Plan

#### ~~6.1.1 Test Plan Startbedingungen~~

#### ~~6.1.2 Test Plan Endbedingungen~~

## 7. Deliverables

## 7.1 Test Evaluation Summaries

Das Projekt umfasst zum jetzigen Zeitpunkt eine Reihe von Unit-Tests, Integrationstests und Frontend-Tests werden in Kürze folgen, welche jeweils manuell ausgeführt werden, bevor Änderungen in unserem Mono-Repo für Backend und Frontend publiziert werden.

## ~~7.2 Reporting on Test Coverage~~

## ~~7.3 Perceived Quality Reports~~

## 7.4 Incident Logs

Bestehen Fehler beim Buildvorgang, so wird diese Version nicht publiziert und das Team stimmt sich intern über das weitere Vorgehen ab, um diese Fehler zu beheben. Für diesen Zweck werder 


## 8. Testing Workflow

1) Lokale Tests in Visual Studio durch Ausführen von `dotnet test`

## 9. Umgebung

### 9.1 Systemanforderungen

Die nachfolgende Tabelle stellt die Systemanforderungen dar, welche die Tests benötigen.

| Resource              | Quantity | Name and Type                |
|-----------------------|:--------:|------------------------------|
| lokales Testsystem    |    1     | Laptop       |

### 9.2 Base Software Elements in the Test Environment

Nachfolgend enumerierte Softwarelösungen werden für das Testen benötigt:

- Visual Studio 2022
- NUnit 4.3
- Selenium
- Moq

### ~~9.3 Produktivitäts- und Unterstützungstoolings~~

## 10. Verantwortlichkeiten und Rollenverteilung

### 10.1 Rollenverteilung

| Rolle          | Person |  Anmerkungen |
|---------------|:--------------:|----------------------------------------|
| Test Manager | Alex, Yahya | Stellen Managementüberblicke zum Testverfahren, sowie den dabei verwendeten Technologien bereit. |
| Test Designer | Alina, Lukas | Liefern die Implementation der verschiedenen Testtypen. |
| Test System Administrator | Moumen | Stellt sicher, dass Tests gewartet und auf Systemänderungen angepasst werden zur Wahrung der Testabdeckung und damit der Softwarequalität. |

### ~~10.2 Staffing and Training Needs~~

## 11. Iteration Milestones

Wir möchten, dass unsere Tests schrittweise eine Quellcodeabdeckung von mindestens 70% des Backend-Quellcodes für Unit-Tests und mindestens für alle kritischen Systemfunktionen bei Integrationstests erreichen, worauf wir schrittweise und inkrementell hinarbeiten, um diese Meilensteine spätestens zum Abschluss des Projekts zu erreichen.

## 12. Risiken, Abhängigkeiten, Annahmen, Einschränkungen 

Unsere Risiken, Abhängigkeiten, Annahmen, Einschränkungen etc. haben wir euch bereits detailliert in unserer [Risikoevaluationstabelle](https://github.com/AlinaBoess/SoftwareEngineeringProjektTINF23B5/blob/main/documentation/RMMM_Risiken_Restaurantprojekt.xlsx) dargestellt.

## ~~13. Management Process and Procedures~~
