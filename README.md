# RevUp
<svg viewBox="0 0 100 100" width="20mm" height="20mm">
  <rect width="100" height="100" rx="26" fill="#112320"/>
  <path d="M 25.96 74.04 A 34 34 0 1 1 74.04 74.04" fill="none" stroke="#CBD9D7" stroke-width="8" stroke-linecap="round"/>
  <line x1="50" y1="50" x2="68.38" y2="31.62" stroke="#00DBAC" stroke-width="9" stroke-linecap="round"/>
  <circle cx="50" cy="50" r="6" fill="#CBD9D7"/>
</svg>

Web-App zur Organisation von Car- und Bike-Meets. Projekt für Modul 426 «Software mit agilen Methoden entwickeln» (3. Lehrjahr), Einzelarbeit.

## Idee

**Problem:** Car- und Bike-Meets werden meist über Chatgruppen oder soziale Medien organisiert. Der Standort wird geteilt, bevor der Veranstalter weiss, wer kommt. Das führt zu ungebetenen Gästen, Lärm und Ärger mit Anwohnern und Polizei.

**Zielgruppe:** Veranstalter (Privatpersonen, Event-Organisatoren) und Teilnehmer aus der Auto- und Motorradszene.

**Lösung:** Veranstalter erstellen Meets mit Thema, Zeitmodus und Limits, öffentlich oder privat. Den Standort sehen Teilnehmer erst nach dem Beitritt (öffentlich) oder nach der Freigabe durch den Veranstalter (privat).

**Vision:** Die einfachste Art, ein Car- oder Bike-Meet sicher und kontrolliert zu organisieren, ohne Chatgruppen und ohne unkontrollierte Standortweitergabe.

## Umfang

Umgesetzt sind US-01 bis US-10: Meets erstellen, bearbeiten und absagen, öffentlich oder privat, Liste mit Filter nach Thema, Beitritt mit Namen, Anfragen annehmen oder ablehnen und freie Plätze live. Polizei-Funktionen und das Melden von Störenfrieden sind bewusst ausgeklammert. Details im [Product Backlog](Docs/01-BACKLOG.md).

## Starten

Voraussetzung ist das .NET 10 SDK.

```
dotnet run --project RevUp
dotnet test RevUp.slnx
```

Beim ersten Start legt die App `RevUp/revup.db` an und füllt sie mit Beispiel-Meets.

## Technik

- C#, .NET 10, ASP.NET Core Blazor (Server-Interaktivität)
- linq2db mit SQLite; fehlende Tabellen und Spalten werden beim Start ergänzt
- xUnit, StyleCop (Build ohne Warnungen), CI mit GitHub Actions bei jedem Push und Pull Request

| Projekt | Zweck |
|---|---|
| `RevUp/` | Blazor-Web-App (App-Icons in `RevUp/Icons/`) |
| `RevUp.Tests/` | Unit-Tests ohne Datenbank, mit eigenen Fakes |
| `RevUp.IntegrationTests/` | Tests gegen SQLite; jeder Test erhält eine frische Kopie von `Snapshot/revup.db` (erzeugt mit `dotnet run --no-cache --file tools/create-snapshot.cs`) |
| `Docs/` | Leeres SDK-Projekt, das die Dokumente in Rider anzeigt |

## Dokumente

Die Dokumente sind nummeriert und oben auf jeder Seite mit Zurück/Weiter verlinkt.

| # | Dokument | Inhalt |
|---|---|---|
| 01 | [Product Backlog](Docs/01-BACKLOG.md) | User Stories, Meilensteine, Vorgehen |
| 02 | [KI-Protokoll](Docs/02-KI-PROTOKOLL.md) | Wichtigste Anfragen an die KI |
| 03 | [Konzept](Docs/03-KONZEPT.md) | Ursprüngliche Idee mit Risiken und offenen Fragen |
| 04 | [Design-Grundlagen](Docs/Design/04-DESIGN.md) | Farben, Typografie, Abstände |
| 05 | [Icon-Konzept](Docs/Design/05-gauge-mark.html) | App-Icon in Grössen und Varianten |
| 06 | [Fallbeschreibung](Docs/UX/06-FALLBESCHREIBUNG.md) | Ausgangslage und Fragenliste |
| 07 | [Ziele](Docs/UX/07-ZIELE.md) | Nutzerziele von Veranstalter und Teilnehmer |
| 08 | [Personas](Docs/UX/08-PERSONAS.md) | Personas und Szenarien |
| 09 | [Navigation](Docs/UX/09-NAVIGATION.md) | Card Sorting und Navigationsstruktur |
| 10 | [Wireframes](Docs/UX/10-wireframes.html) | Low-Fi-Wireframes der Hauptseiten |
| 11 | [Prototyp-Test](Docs/UX/11-PROTOTYP-TEST.md) | Selbsttest gegen die Wireframes |
| 12 | [Lösungsdokument](Docs/12-LOESUNGSDOKUMENT.md) | Stories, Patterns, Branches, TDD-Nachweis, SOLID, Retrospektive |
| – | [Prototyp](Docs/UX/Prototype/README.md) | Klickbarer Hi-Fi-Prototyp |

**Weiter →** [01 · Product Backlog](Docs/01-BACKLOG.md)