# KI-Protokoll: RevUp (M426)

*Dokument 2 von 12 · [← 01 Backlog](01-BACKLOG.md) · Weiter: [03 Konzept →](03-KONZEPT.md)*

Gemäss Vorgabe LB: festgehalten werden die wichtigsten **Prompts** (nicht die Antworten).

| Datum      | Prompt (sinngemäss)                                                                                                                                                                   |
|------------|---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| 2026-08-20 | Brainstorming-Anfrage für ein Projekt für M426, keine Vorgabe zu Thema/Team/Stack.                                                                                                    |
| 2026-08-20 | Vorstellung der Idee "Car-Meet App" mit detaillierten Features (öffentlich/privat, Themed Meets, Polizei-Feature, Zeitmodelle, Limits).                                               |
| 2026-08-20 | Anfrage nach Pro/Contra, Setup und Publishing-Aufwand für Blazor vs. .NET MAUI vs. WPF für die Umsetzung in C#.                                                                       |
| 2026-08-20 | Entscheid für Blazor/ASP.NET Core; Vorgabe der festen Arbeits-/Codestruktur (`3. Lehrjahr\Code\<Projektname>`).                                                                       |
| 2026-08-20 | Auftrag: Blazor-Projekt "RevUp" scaffolden, README und Product Backlog mit User Stories erstellen.                                                                                    |
| 2026-08-22 | Icon-Konzept nach Vorbild eines Referenz-Icon-Sets (Squircle, Glyph, Akzentfarbe) entwerfen, keine Lucide-Icons.                                                                      |
| 2026-08-28 | Frage, ob vor Repo-Erstellung ein Wireframe/Anforderungsschema nötig ist; Auftrag, das mitgelieferte UCD-Schema (Fallbeschreibung bis Prototyp-Test) leichtgewichtig durchzuarbeiten. |
| 2026-08-28 | Auftrag: für den Prototyp Claude Design ausprobieren; 4 Hauptscreens erst statisch, dann klickbar, nach plattformunabhängigen Human-Interface-Guidelines-Prinzipien.                  |
| 2026-08-28 | Feedback: Scrollbar auf Entdecken-Screen sichtbar, sollte versteckt werden; Palette zusätzlich Richtung Apple-HIG-Farbprinzipien anpassen, soweit nötig, Theme aber beibehalten.      |
| 2026-08-28 | Auftrag: Vorgehen dokumentieren, danach mit dem funktionalen (klickbaren) Prototyp weitermachen.                                                                                      |
| 2026-08-28 | Korrektur: Prototyp soll rein lokal sein, kein Claude-Link, keine geteilte Live-Version.                                                                                              |
| 2026-08-28 | Präzisierung: Claude Design darf intern weiterverwendet werden, es soll nur nicht die einzige Doku sein und nicht öffentlich geteilt werden.                                          |
| 2026-08-28 | Auftrag: alle Docs chronologisch nummerieren, zusätzlich Zurück/Weiter-Links zwischen den Seiten.                                                                                     |
| 2026-10-10 | Abgleich von RevUp mit den Vorgaben der LB; Vorschlag, Erkenntnisse aus einem anderen eigenen Projekt (Architektur, Designsprache, CI, Testaufbau) zu übernehmen.                     |
| 2026-10-12 | Auftrag: bestehende Entdecken-Seite behalten, technisches Fundament aufsetzen (CI, Vorlagen, Story-IDs, Vorgehen), Stories als GitHub-Issues anlegen und mit US-01 beginnen.         |
| 2026-10-13 | Frage, wie das Projekt-Board (GitHub Projects) eingerichtet wird; Link zum Board im Backlog ergänzt.                                                                                       |
| 2026-10-13 | Wunsch, automatisch eingefügte KI-Hinweise in Issues und Pull Requests zu entfernen.                                                                                                  |
| 2026-10-15 | Auftrag: mit US-02 (Sichtbarkeit öffentlich/privat) weitermachen.                                                                                                                     |
| 2026-10-15 | Auftrag: mit US-03 (Meet-Liste) weitermachen.                                                                                                                                         |
| 2026-10-15 | Vorgabe einer erweiterten `.editorconfig` mit strengeren Code-Style-Regeln; Code daran anpassen.                                                                                         |
| 2026-10-15 | Vorgaben aus einer separaten Sitzung übernommen (Listen-Icons, Persistenz mit linq2db/SQLite, async, Basisklassen, Unit- und Integrationstests); als technische Aufgaben T-01 bis T-03 erfasst. |
| 2026-10-15 | Rider-Hinweise in den Testprojekten beheben; danach mit US-04 (öffentlichem Meet beitreten) weitermachen.                                                                            |
| 2026-10-15 | Frage, ob Konten wie in einem anderen eigenen Projekt (UtilityTools) eingeführt werden sollen; Entscheid für eine schlanke Variante (US-17 – US-19), Zwei-Faktor und Passkeys als Stretch (US-20). |
| 2026-10-29 | Abgabetermin (01.11.) und Masterplan geteilt; Sprint 1 endet am 29.10., Sprint 2 am 30.10. Priorisierung: US-05/US-06 mit Observer, danach SOLID, Retrospektive und Abgabe. |
| 2026-10-29 | Auftrag: mit US-05 (Beitrittsanfrage für privates Meet) weitermachen.                                                                                                                   |
| 2026-10-29 | Auftrag: US-06 (Anfragen annehmen/ablehnen) mit dem Observer-Pattern für Live-Aktualisierung umsetzen.                                                                       |
| 2026-10-29 | Entscheid: nicht bewertungsrelevante Stories (Konten, P2/P3 ausser US-10) in den Meilenstein «Nach der LB» verschieben; Sprint 1 abschliessen.                         |
| 2026-10-29 | Auftrag: US-10 (freie Plätze) als eigene Story umsetzen.                                                                                                                      |
| 2026-10-29 | Hinweis auf Krankheit (21.–27.10.); Auftrag: Lösungsdokument mit Patterns, Branches, TDD-Nachweis, SOLID-Beurteilung, Sprint-Reviews und Retrospektive entwerfen.             |
| 2026-10-29 | Abgabe mit der Lehrperson auf den 02.11. verschoben; US-07 bis US-09 zurück in Sprint 2 (US-07/US-08 heute, US-09 morgen mit den Dokumenten).                       |
| 2026-10-29 | Auftrag: US-07 (Filter nach Thema, kombinierbar mit öffentlich/privat) umsetzen.                                                                                         |
| 2026-10-29 | Auftrag: US-08 (Zeitmodell Fixed / Open Start / Open End wählen) umsetzen.                                                                                                  |
| 2026-10-29 | Merge-Konflikte (KI-Protokoll) in den offenen PRs zu US-07, US-08 und US-10 auflösen. |
| 2026-10-29 | Kurze Gesamtprüfung nach dem Mergen (Build, Tests, CI, Links, App) und Frage, wo US-07/US-08 in der Doku fehlen. |
| 2026-10-30 | Auftrag: US-09 (Meet bearbeiten oder absagen) umsetzen. |
| 2026-11-01 | Board-Screenshots von Sprint 2 zu einem Bild zusammenfügen und im Sprint-Review einbinden, Meilenstein Sprint 2 abschliessen. |

*Hinweis: Dieses Protokoll wird laufend ergänzt, sobald weitere KI-Unterstützung für das Projekt genutzt wird.*