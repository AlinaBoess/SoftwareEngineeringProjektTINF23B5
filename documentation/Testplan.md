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

