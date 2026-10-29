# Product Backlog: RevUp

*Dokument 1 von 12 · [↑ Start](../README.md) · Weiter: [02 KI-Protokoll →](02-KI-PROTOKOLL.md)*

## Meilensteine (gemäss Lernkompass M426)

| Phase | Meilenstein              | Status                                                                                           |
|-------|--------------------------|--------------------------------------------------------------------------------------------------|
| 1     | Projektidee & Team       | <span style="color: green; font-weight: bold;">&#x2611;&#xFE0E;</span> [README.md](../README.md) |
| 2     | Vision & Product Backlog | <span style="color: green; font-weight: bold;">&#x2611;&#xFE0E;</span> dieses Dokument           |
| 3     | Technisches Fundament    | <span style="color: green; font-weight: bold;">&#x2611;&#xFE0E;</span> CI, Vorlagen, Story-IDs, Vorgehen (siehe unten) |
| 4     | Sprint 1 (bis 29.10.)    | <span style="color: green; font-weight: bold;">&#x2611;&#xFE0E;</span> US-01 – US-06, T-01 – T-03; Konten (US-17 – US-19) bewusst verschoben |
| 5     | Sprint 2 (30.10.)        | <span style="color: red; font-weight: bold;">&#x2612;&#xFE0E;</span> US-10, Review, Retrospektive, SOLID-Beurteilung, Abgabe |
| 6     | Nach der LB              | <span style="color: red; font-weight: bold;">&#x2612;&#xFE0E;</span> US-07 – US-09, US-11, US-12, US-17 – US-19 |

## User Stories (MVP)

Priorisiert (P1 = zuerst umsetzen). Format: *Als \<Rolle> möchte ich \<Ziel>, damit \<Nutzen>.*

| ID    | Prio | User Story                                                                                                                                                                    | Akzeptanzkriterien (Kurzform)                                                                             |
|-------|------|-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|-----------------------------------------------------------------------------------------------------------|
| US-01 | P1   | Als Veranstalter möchte ich ein neues Meet mit Titel, Thema, Ort, Zeitmodus, max. Teilnehmerzahl und max. Lautstärke erstellen, damit ich einen Treffpunkt organisieren kann. | Meet wird gespeichert; alle Pflichtfelder validiert; Meet erscheint in der Übersicht.                     |
| US-02 | P1   | Als Veranstalter möchte ich ein Meet als "Öffentlich" oder "Privat" markieren, damit ich steuern kann, wer den Standort sieht.                                                | Sichtbarkeits-Flag beeinflusst nachgelagerten Beitritts-Flow (US-04/US-05).                                 |
| US-03 | P1   | Als Teilnehmer möchte ich alle aktuellen Meets in einer Liste sehen, damit ich ein passendes Meet finden kann.                                                                | Liste zeigt Titel, Thema, Zeitfenster, Sichtbarkeit (öffentlich/privat), ohne genauen Standort.           |
| US-04 | P1   | Als Teilnehmer möchte ich einem öffentlichen Meet mit meinem Namen beitreten und danach sofort den Standort sehen, damit ich hinfahren kann.                                  | Nach Angabe Name + Bestätigung wird Standort angezeigt; Teilnehmerzahl wird erhöht (max. Limit beachten). |
| US-05 | P1   | Als Teilnehmer möchte ich bei einem privaten Meet eine Beitrittsanfrage mit meinem Namen stellen, damit der Veranstalter über meine Teilnahme entscheiden kann.               | Anfrage landet in Pending-Liste des Veranstalters; Standort ist für den Anfragenden noch nicht sichtbar.  |
| US-06 | P1   | Als Veranstalter möchte ich offene Beitrittsanfragen zu meinem privaten Meet annehmen oder ablehnen, damit ich die Kontrolle über die Teilnehmerliste behalte.                | Bei Annahme wird Standort für die Person sichtbar; bei Ablehnung nicht.                                   |
| US-07 | P2   | Als Teilnehmer möchte ich Meets nach Thema filtern (z. B. 90s Cars, Tuning, Motorräder), damit ich schneller passende Meets finde.                                            | Filter reduziert die Liste korrekt; kombinierbar mit Status (öffentlich/privat).                          |
| US-08 | P2   | Als Veranstalter möchte ich ein Zeitmodell wählen (Fixed / Open Start / Open End), damit klar ist, wann das Meet stattfindet.                                                 | Zeitmodell wird bei Erstellung gewählt und in der Detailansicht korrekt dargestellt.                      |
| US-09 | P2   | Als Veranstalter möchte ich mein Meet bearbeiten oder absagen, damit ich auf Änderungen reagieren kann.                                                                       | Änderungen/Absage werden für Teilnehmer sichtbar/nachvollziehbar.                                         |
| US-10 | P2   | Als Teilnehmer möchte ich sehen, wie viele Plätze bei einem Meet noch frei sind, damit ich weiss, ob sich eine Anfrage lohnt.                                                 | Anzeige "X / max. Teilnehmer" korrekt und live aktualisiert.                                              |
| US-11 | P3   | Als Veranstalter möchte ich meine erstellten Meets in einer eigenen Übersicht sehen, damit ich sie verwalten kann.                                                            | Liste "Meine Meets" zeigt nur eigene Einträge inkl. Status der Anfragen.                                  |
| US-12 | P3   | Als Teilnehmer möchte ich meine gestellten Beitrittsanfragen und deren Status einsehen, damit ich weiss, ob ich zugelassen wurde.                                             | Liste "Meine Anfragen" mit Status (offen/angenommen/abgelehnt).                                           |
| US-17 | P1   | Als Besucher möchte ich ein Konto mit Benutzername und Passwort erstellen, damit meine Meets, Beitritte und Anfragen eindeutig mir gehören. | Benutzername eindeutig (3–30 Zeichen); Anzeigename; Passwort mind. 8 Zeichen, nur als Hash gespeichert; nach der Registrierung angemeldet. |
| US-18 | P1   | Als Nutzer möchte ich mich an- und abmelden, damit niemand sonst in meinem Namen Meets erstellt oder beitritt. | Anmeldung mit Benutzername und Passwort; falsche Angaben ergeben eine allgemeine Meldung; angemeldet bleiben über ein Cookie; Abmelden; zu viele Fehlversuche werden kurz gesperrt. |
| US-19 | P1   | Als Veranstalter möchte ich, dass nur angemeldete Personen beitreten und den Ort sehen, damit niemand mit einem fremden Namen zum Standort kommt. | Erstellen und Beitreten nur angemeldet (sonst zur Anmeldung); Veranstalter und Teilnehmer werden über das Konto gespeichert und mit Anzeigename gezeigt; den Ort sehen nur Veranstalter und beigetretene Konten. |

## Umsetzung

Jede Story ist ein GitHub-Issue mit ihrer ID am Anfang des Titels (bei US-01 bis US-12 zusätzlich Issue-Nummer = Story-Nummer), Label `user story`
und Priorität (`P1` bis `P3`). Die Sprints sind Meilensteine: Sprint 1 = P1, Sprint 2 = P2, Sprint 3 = P3.
Der Stand jeder Story ist auf dem [Board](https://github.com/users/Tojioo/projects/2/views/1) sichtbar
(Backlog, Sprint Backlog, In Progress, Review, Done).

**Definition of Ready:** Story im Format «Als … möchte ich …, damit …», eindeutige ID, prüfbare Akzeptanzkriterien, Priorität.

**Ablauf pro Story (TDD):**

1. Branch von `main` mit der Story-ID: `US-01-meet-erstellen`.
2. Test schreiben, der die Akzeptanzkriterien prüft, committen und pushen. Die CI (GitHub Actions) zeigt ihn rot.
3. Einfachste Implementierung, bis der Test grün ist, committen und pushen.
4. Aufräumen (Refactoring), Tests bleiben grün. Schritte 2 bis 4 wiederholen, bis alle Kriterien abgedeckt sind.
5. Pull Request nach `main` mit `Closes #<Nr>`, mit Merge Commit mergen (kein Squash, damit die Test-Commits sichtbar bleiben).
   Branches werden nicht gelöscht.

**Konventionen:** Commits `US-xx: <Satz im Imperativ>` (Englisch, wie der Code); Test-Commits beginnen mit `Test`.
Code nach `.editorconfig` und StyleCop, der Build bleibt ohne Warnungen.

**Definition of Done:** Tests vor der Funktionalität gepusht und dort rot; alle Akzeptanzkriterien erfüllt und getestet;
CI grün ohne Warnungen; Coding Conventions, SOLID und Code Smells geprüft; Backlog und KI-Protokoll nachgeführt.

**Geplante Design Patterns:**

- **Observer:** Änderungen an einem Meet (neue Anfrage, Freigabe, freie Plätze) benachrichtigen die geöffneten Seiten,
  damit Veranstalter offene Anfragen (US-06) und alle die freien Plätze (US-10) live sehen, ohne neu zu laden.
- **Strategy:** Eine Beitritts-Regel pro Sichtbarkeit: öffentlich = sofort beitreten und Standort sehen (US-04),
  privat = Anfrage stellen und auf Freigabe warten (US-05). So verteilt sich die Unterscheidung öffentlich/privat nicht
  über den Code, und weitere Regeln (z. B. «nur auf Einladung») kommen ohne Änderung bestehender Klassen dazu.

## Technische Aufgaben

Keine User Stories: Sie liefern keine neue Funktion für Veranstalter oder Teilnehmer, sondern verbessern Darstellung,
Architektur oder Testbarkeit. Ablauf wie bei den Stories (Issue, Branch mit ID, Pull Request). Bei reinem Refactoring
bleiben die bestehenden Tests vor und nach jedem Schritt grün.

| ID   | Aufgabe                                                                                                                                       | Grund                                                                                    |
|------|-----------------------------------------------------------------------------------------------------------------------------------------------|------------------------------------------------------------------------------------------|
| T-01 | Icons für Zeit und Ort auf den Meet-Karten (Kalender, Standort-Pin), gestaltet wie das bestehende Teilnehmer-Icon.                            | Schnellere Orientierung in der Liste. Icons sind dekorativ (`aria-hidden`), immer mit Text. |
| T-02 | Persistenz mit linq2db und SQLite statt Speicherung im Arbeitsspeicher; asynchrone Service-Methoden; Basisklassen für Entitäten und Datenkontext. | Meets bleiben nach einem Neustart erhalten. Grundlage für US-04 bis US-06.               |
| T-03 | Testaufbau: Unit-Tests ohne I/O mit eigenen Fakes; neues Projekt `RevUp.IntegrationTests`, das pro Test einen SQLite-Snapshot wiederherstellt. | Datenbank-Code wird getestet, ohne die schnellen Unit-Tests zu verlangsamen.             |

## Später / Stretch (bewusst nicht im MVP)

Bewusste Scope-Entscheidung: Diese Punkte sind fachlich Teil der ursprünglichen Idee, würden den MVP-Rahmen aber sprengen (siehe README, Abschnitt "Bewusst ausgeklammert").

| ID    | User Story                                                                                                                                                       | Grund für Verschiebung                                                 |
|-------|------------------------------------------------------------------------------------------------------------------------------------------------------------------|------------------------------------------------------------------------|
| US-13 | Als Polizei möchte ich alle gemeldeten Meets in meiner Umgebung sehen, damit ich bei Bedarf eingreifen kann.                                                     | Eigene Rolle + Verifikationslogik, hoher Aufwand, datenschutzrelevant. |
| US-14 | Als Veranstalter möchte ich mein Meet bei der Polizei anmelden/bewilligen lassen, damit es offiziell abgesichert ist.                                            | Setzt US-13 (Polizei-Rolle) voraus.                                 |
| US-15 | Als Polizei möchte ich bei Lärmbeschwerden eine Warnung oder Räumungsankündigung an den Veranstalter senden, damit Probleme vor Ort schnell kommuniziert werden. | Setzt US-13/US-14 voraus; Echtzeit-Kommunikation nötig.                |
| US-16 | Als Veranstalter möchte ich eine Person als "störend" melden, die am Standort markiert wird, damit andere Teilnehmer/die Polizei informiert sind.                | Moderationslogik + Missbrauchspotenzial, braucht eigenes Konzept.      |
| US-20 | Als Nutzer möchte ich mein Konto zusätzlich mit Zwei-Faktor-Anmeldung oder Passkey schützen, damit ein gestohlenes Passwort nicht reicht. | Sinnvoll, aber für den MVP zu umfangreich (Authenticator-App, Wiederherstellungscodes, WebAuthn). |

## Nicht-funktionale Anforderungen (Auszug)

- Standortdaten dürfen erst nach den definierten Freigabe-Regeln (öffentlich/privat) ausgeliefert werden. Serverseitig durchgesetzt, nicht nur UI-seitig versteckt.
- Alle Formulareingaben serverseitig validiert (nicht nur clientseitig).