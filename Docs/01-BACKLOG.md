# Product Backlog: RevUp

*Dokument 1 von 11 · [↑ Start](../README.md) · Weiter: [02 KI-Protokoll →](02-KI-PROTOKOLL.md)*

## Meilensteine (gemäss Lernkompass M426)

| Phase | Meilenstein              | Status                                                                                           |
|-------|--------------------------|--------------------------------------------------------------------------------------------------|
| 1     | Projektidee & Team       | <span style="color: green; font-weight: bold;">&#x2611;&#xFE0E;</span> [README.md](../README.md) |
| 2     | Vision & Product Backlog | <span style="color: green; font-weight: bold;">&#x2611;&#xFE0E;</span> dieses Dokument           |
| 3+    | Umsetzung MVP            | <span style="color: red; font-weight: bold;">&#x2612;&#xFE0E;</span> offen                       |

## User Stories (MVP)

Priorisiert (P1 = zuerst umsetzen). Format: *Als \<Rolle> möchte ich \<Ziel>, damit \<Nutzen>.*

| #  | Prio | User Story                                                                                                                                                                    | Akzeptanzkriterien (Kurzform)                                                                             |
|----|------|-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|-----------------------------------------------------------------------------------------------------------|
| 1  | P1   | Als Veranstalter möchte ich ein neues Meet mit Titel, Thema, Ort, Zeitmodus, max. Teilnehmerzahl und max. Lautstärke erstellen, damit ich einen Treffpunkt organisieren kann. | Meet wird gespeichert; alle Pflichtfelder validiert; Meet erscheint in der Übersicht.                     |
| 2  | P1   | Als Veranstalter möchte ich ein Meet als "Öffentlich" oder "Privat" markieren, damit ich steuern kann, wer den Standort sieht.                                                | Sichtbarkeits-Flag beeinflusst nachgelagerten Beitritts-Flow (Story 4/5).                                 |
| 3  | P1   | Als Teilnehmer möchte ich alle aktuellen Meets in einer Liste sehen, damit ich ein passendes Meet finden kann.                                                                | Liste zeigt Titel, Thema, Zeitfenster, Sichtbarkeit (öffentlich/privat), ohne genauen Standort.           |
| 4  | P1   | Als Teilnehmer möchte ich einem öffentlichen Meet mit meinem Namen beitreten und danach sofort den Standort sehen, damit ich hinfahren kann.                                  | Nach Angabe Name + Bestätigung wird Standort angezeigt; Teilnehmerzahl wird erhöht (max. Limit beachten). |
| 5  | P1   | Als Teilnehmer möchte ich bei einem privaten Meet eine Beitrittsanfrage mit meinem Namen stellen, damit der Veranstalter über meine Teilnahme entscheiden kann.               | Anfrage landet in Pending-Liste des Veranstalters; Standort ist für den Anfragenden noch nicht sichtbar.  |
| 6  | P1   | Als Veranstalter möchte ich offene Beitrittsanfragen zu meinem privaten Meet annehmen oder ablehnen, damit ich die Kontrolle über die Teilnehmerliste behalte.                | Bei Annahme wird Standort für die Person sichtbar; bei Ablehnung nicht.                                   |
| 7  | P2   | Als Teilnehmer möchte ich Meets nach Thema filtern (z. B. 90s Cars, Tuning, Motorräder), damit ich schneller passende Meets finde.                                            | Filter reduziert die Liste korrekt; kombinierbar mit Status (öffentlich/privat).                          |
| 8  | P2   | Als Veranstalter möchte ich ein Zeitmodell wählen (Fixed / Open Start / Open End), damit klar ist, wann das Meet stattfindet.                                                 | Zeitmodell wird bei Erstellung gewählt und in der Detailansicht korrekt dargestellt.                      |
| 9  | P2   | Als Veranstalter möchte ich mein Meet bearbeiten oder absagen, damit ich auf Änderungen reagieren kann.                                                                       | Änderungen/Absage werden für Teilnehmer sichtbar/nachvollziehbar.                                         |
| 10 | P2   | Als Teilnehmer möchte ich sehen, wie viele Plätze bei einem Meet noch frei sind, damit ich weiss, ob sich eine Anfrage lohnt.                                                 | Anzeige "X / max. Teilnehmer" korrekt und live aktualisiert.                                              |
| 11 | P3   | Als Veranstalter möchte ich meine erstellten Meets in einer eigenen Übersicht sehen, damit ich sie verwalten kann.                                                            | Liste "Meine Meets" zeigt nur eigene Einträge inkl. Status der Anfragen.                                  |
| 12 | P3   | Als Teilnehmer möchte ich meine gestellten Beitrittsanfragen und deren Status einsehen, damit ich weiss, ob ich zugelassen wurde.                                             | Liste "Meine Anfragen" mit Status (offen/angenommen/abgelehnt).                                           |

## Später / Stretch (bewusst nicht im MVP)

Bewusste Scope-Entscheidung: Diese Punkte sind fachlich Teil der ursprünglichen Idee, würden den MVP-Rahmen aber sprengen (siehe README, Abschnitt "Bewusst ausgeklammert").

| #  | User Story                                                                                                                                                       | Grund für Verschiebung                                                 |
|----|------------------------------------------------------------------------------------------------------------------------------------------------------------------|------------------------------------------------------------------------|
| 13 | Als Polizei möchte ich alle gemeldeten Meets in meiner Umgebung sehen, damit ich bei Bedarf eingreifen kann.                                                     | Eigene Rolle + Verifikationslogik, hoher Aufwand, datenschutzrelevant. |
| 14 | Als Veranstalter möchte ich mein Meet bei der Polizei anmelden/bewilligen lassen, damit es offiziell abgesichert ist.                                            | Setzt Story 13 (Polizei-Rolle) voraus.                                 |
| 15 | Als Polizei möchte ich bei Lärmbeschwerden eine Warnung oder Räumungsankündigung an den Veranstalter senden, damit Probleme vor Ort schnell kommuniziert werden. | Setzt Story 13/14 voraus; Echtzeit-Kommunikation nötig.                |
| 16 | Als Veranstalter möchte ich eine Person als "störend" melden, die am Standort markiert wird, damit andere Teilnehmer/die Polizei informiert sind.                | Moderationslogik + Missbrauchspotenzial, braucht eigenes Konzept.      |

## Nicht-funktionale Anforderungen (Auszug)

- Standortdaten dürfen erst nach den definierten Freigabe-Regeln (öffentlich/privat) ausgeliefert werden. Serverseitig durchgesetzt, nicht nur UI-seitig versteckt.
- Alle Formulareingaben serverseitig validiert (nicht nur clientseitig).