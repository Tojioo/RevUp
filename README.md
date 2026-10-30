# RevUp
<svg viewBox="0 0 100 100" width="20mm" height="20mm">
  <rect width="100" height="100" rx="26" fill="#112320"/>
  <path d="M 25.96 74.04 A 34 34 0 1 1 74.04 74.04" fill="none" stroke="#CBD9D7" stroke-width="8" stroke-linecap="round"/>
  <line x1="50" y1="50" x2="68.38" y2="31.62" stroke="#00DBAC" stroke-width="9" stroke-linecap="round"/>
  <circle cx="50" cy="50" r="6" fill="#CBD9D7"/>
</svg>

Web-App zur Organisation von Car- und Bike-Meets. Projekt für Modul 426 "Software mit agilen Methoden entwerfen" (3. Lehrjahr).

## Projektidee & Team

**Team:** Solo-Projekt (Einzelarbeit)

**Problem:** Car- und Bike-Meets werden heute meist informell über Chatgruppen oder soziale Medien organisiert. Das führt zu mehreren Problemen:
- Standorte werden öffentlich geteilt, bevor der Veranstalter weiss, wer überhaupt kommt → ungebetene Gäste, Störenfriede, teils Lärmbelästigung und Ärger mit Anwohnern/Polizei.
- Es gibt keine strukturierte Möglichkeit, ein Meet nach Thema, Zeitrahmen oder Teilnehmerzahl zu organisieren.
- Veranstalter haben keine Kontrolle darüber, wer am Meet teilnimmt, bevor der Standort bekannt ist.

**Zielgruppe:**
- **Veranstalter:** Privatpersonen oder Event-Organisatoren, die ein Car-/Bike-Meet durchführen wollen.
- **Teilnehmer:** Personen aus der Auto-/Motorrad-Szene, die an Meets teilnehmen möchten.

**Lösung (Kurzfassung):** RevUp lässt Veranstalter Meets erstellen (öffentlich oder privat, mit Thema, Zeitrahmen und Teilnehmerlimits). Der genaue Standort wird erst nach Angabe der Kontaktdaten (öffentliche Meets) bzw. nach expliziter Freigabe durch den Veranstalter (private Meets) sichtbar. Das reduziert die Hemmschwelle für offene Standortangaben und gibt Veranstaltern Kontrolle über ihre Teilnehmerliste.

## Vision

RevUp soll die einfachste Art werden, ein Car-/Bike-Meet sicher und kontrolliert zu organisieren.
Von der Ankündigung bis zur An-/Abmeldung der Teilnehmer, ohne Umweg über Chatgruppen und ohne unkontrollierte Standortweitergabe.

## Tech-Stack

- **ASP.NET Core + Blazor** (Server-Interaktivität), C#
- .NET 10
- Datenhaltung: linq2db mit SQLite (`revup.db`, Connection String `RevUp` in `appsettings.json`); fehlende Tabellen werden beim Start angelegt, eine leere Datenbank mit Beispiel-Meets gefüllt
- Tests: xUnit, CI: GitHub Actions (`dotnet test RevUp.slnx` bei jedem Push und Pull Request)
- Code-Analyse: StyleCop, Build ohne Warnungen

## Umfang (MVP)

Siehe [Docs/01-BACKLOG.md](Docs/01-BACKLOG.md) für den vollständigen Product Backlog inkl. User Stories.

**MVP deckt ab:**
- Meet erstellen (Titel, Thema, Ort, Zeitmodus, max. Teilnehmer, max. Lautstärke)
- Öffentlich/Privat-Unterscheidung
- Beitreten mit Namensangabe → öffentlich: Standort sofort sichtbar; privat: erst nach Freigabe durch Veranstalter
- Meet-Übersicht mit Filter nach Thema
- Veranstalter kann Beitrittsanfragen annehmen/ablehnen

**Bewusst ausgeklammert (Backlog, nicht MVP):** Polizei-Rolle, Lärmbeschwerden, Räumungsankündigungen, Melden von Störenfrieden.
Siehe Backlog, Abschnitt "Später/Stretch".

## Solution-Struktur

Die Solution ist in 4 Projekte unterteilt:

| Projekt | Zweck |
|---|---|
| `RevUp/` | Die eigentliche Blazor-Web-App (`RevUp.csproj`) |
| `RevUp.Tests/` | Unit-Tests (xUnit): ohne I/O und Datenbank, mit eigenen Fakes |
| `RevUp.IntegrationTests/` | Integrationstests gegen SQLite: jeder Test erhält eine frische Kopie von `Snapshot/revup.db`. Den Snapshot erzeugt `dotnet run --no-cache --file tools/create-snapshot.cs` mit dem echten Seeder; seine Daten sind auf den Erzeugungstag eingefroren |
| `Docs/` | Kein echtes Projekt, sondern ein leeres SDK-Projekt (`Documentation.csproj`), das nur alle Markdown-/HTML-Dateien unter `Docs/` in Riders Solution Explorer sichtbar macht |

## Projektstruktur

Die Docs sind durchnummeriert und über Zurück/Weiter-Links am Ende jeder Seite chronologisch verlinkt. Einstieg: [Docs/01-BACKLOG.md](Docs/01-BACKLOG.md).

| # | Datei | Inhalt |
|---|---|---|
| 01 | `Docs/01-BACKLOG.md` | Product Backlog, User Stories, Meilensteine |
| 02 | `Docs/02-KI-PROTOKOLL.md` | KI-Protokoll (Prompts gemäss Vorgabe LB) |
| 03 | `Docs/03-KONZEPT.md` | Konzept-Dokument mit Risiken und offenen Fragen |
| 04 | `Docs/Design/04-DESIGN.md` | Design-Grundlagen (Farben, Typografie, Abstände), lose Guideline |
| 05 | `Docs/Design/05-gauge-mark.html` | Icon-Konzept-Übersicht (Grössen/Varianten) |
| 06 | `Docs/UX/06-FALLBESCHREIBUNG.md` | Fallbeschreibung + Fragenliste |
| 07 | `Docs/UX/07-ZIELE.md` | Nutzerziele (Veranstalter, Teilnehmer) |
| 08 | `Docs/UX/08-PERSONAS.md` | Personas + Szenarien |
| 09 | `Docs/UX/09-NAVIGATION.md` | Card Sorting, Navigationsstruktur |
| 10 | `Docs/UX/10-wireframes.html` | Low-Fi-Wireframes der Hauptseiten |
| 11 | `Docs/UX/11-PROTOTYP-TEST.md` | Selbsttest gegen die Wireframes |
| 12 | `Docs/12-LOESUNGSDOKUMENT.md` | Lösungsdokument der LB 426: Stories, Patterns, Branches, TDD-Nachweis, SOLID, Retrospektive |
| – | `Docs/UX/Prototype/README.md` | Hi-Fi-Prototyp: Übersicht, `prototype.html` (lokal, massgeblich) + interne Claude-Design-Arbeitsversion |

`RevUp/Icons/`: finale App-Icon-SVGs (Primary, Alert, Inverted), ausserhalb der nummerierten Doc-Kette.

---

**Weiter →** [01 · Product Backlog](Docs/01-BACKLOG.md)