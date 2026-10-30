# Konzept: Car-Meet App

*Dokument 3 von 12 · [← 02 KI-Protokoll](02-KI-PROTOKOLL.md) · Weiter: [04 Design-Grundlagen →](Design/04-DESIGN.md)*

## Übersicht

Mobile App, mit der Nutzer Car-Meets organisieren und daran teilnehmen können. Kernidee: Standort wird erst nach Angabe von Personalien freigegeben, um Störenfriede und Lärmbelästigung einzudämmen. Zusätzlich gibt es eine Polizei-Integration zur Meldung und Überwachung von Meets.

## Rollen

- **Teilnehmer**: meldet sich für ein Meet an, gibt Personalien an, erhält je nach Meet-Typ direkt oder nach Freigabe den Standort.
- **Veranstalter (privat)**: einzelne Person, die ein Meet organisiert.
- **Veranstalter (gewerblich)**: professioneller Anbieter, evtl. mit erweiterten Funktionen (Bewilligungen, grössere Meets).
- **Polizei**: eigener Profiltyp mit Übersicht über alle Meets in der Umgebung, kann Warnungen/Räumungen aufschalten.

## Kernfunktionen

- Meet erstellen mit Titel, Thema (90s Cars, Motorräder, Green, Tuning, etc.), Zeitraum, Ort.
- Sichtbarkeit: **Öffentlich** (Standort direkt nach Personalien-Angabe sichtbar) oder **Privat** (Veranstalter akzeptiert/lehnt Teilnehmer einzeln ab; Standort erst nach Zusage sichtbar).
- Zeit-Modi: **Fixed** (feste Start-/Endzeit), **Open Start** (Start ab Zeitpunkt X, Standort erst dann sichtbar), **Open End** (kein festes Ende).
- Veranstalter-Limits: max. Lautstärkepegel, max. Teilnehmerzahl.
- Meldefunktion: Veranstalter kann einzelne Teilnehmer als "auffällig" markieren, sichtbar am Standort in der App.

## Polizei-Integration

- Veranstalter kann ein Meet offiziell bei der Polizei anmelden (inkl. Bewilligungsstatus).
- Polizei sieht alle Meets in ihrer Umgebung.
- Bei Lärmbeschwerden aus der Umgebung kann die Polizei eine Warnung oder Räumungsankündigung direkt an den Veranstalter schicken.

## Kritische Punkte und offene Fragen

**Datenschutz der Personalien**
Das Kernkonzept verlangt Personalien vor Standort-Freigabe. Das ist eine hochsensible Datenkategorie (Namen, evtl. Adresse, Ausweisnummer?). Ungeklärt: Wer speichert diese Daten, wie lange, wer hat Zugriff (auch die Polizei automatisch)? Das wirft DSGVO- bzw. (in der Schweiz) DSG-Fragen auf und braucht eine klare Rechtsgrundlage und Löschfristen. Ohne echte Verifizierung (z. B. ID-Scan) ist die Angabe zudem leicht fälschbar und bringt den gewünschten Schutz nicht zwingend.

**Missbrauchspotenzial der Meldefunktion**
Die Möglichkeit, Teilnehmer als "aus der Reihe tanzend" zu markieren, ist subjektiv und ohne Moderation anfällig für Falschmeldungen, Mobbing oder Diskriminierung. Es braucht ein Verfahren für Widerspruch/Review, bevor eine solche Markierung sichtbar wird.

**Haftungsfragen**
Bei Vorfällen am Meet (Unfall, Sachschaden, Auseinandersetzung) ist unklar, wer haftet: App-Betreiber, Veranstalter oder Teilnehmer. Das sollte früh mit AGB/Nutzungsbedingungen und ggf. rechtlicher Beratung geklärt werden, besonders weil private Veranstalter in der Regel keine Versicherung für solche Events haben.

**Echtheit der Polizei-Kooperation**
Ein Polizei-Profiltyp mit Zugriff auf alle Meets setzt eine offizielle Zusammenarbeit mit Polizeibehörden voraus. Das ist kein reines App-Feature, sondern ein organisatorisches/politisches Projekt (Datenaustausch, Zuständigkeiten je Kanton/Gemeinde, Authentifizierung echter Polizeikonten).

**Race Conditions bei Open Start**
Bei "Open Start" muss serverseitig sauber gesteuert werden, wann der Standort für wen sichtbar wird, sonst gibt es Zeitzonen-/Latenzprobleme oder Umgehungsmöglichkeiten (z. B. durch manipulierte Geräteuhrzeit).

**Fehlende Monetarisierung**
Aktuell ist kein Geschäftsmodell definiert (Abo für gewerbliche Veranstalter? Provisionen? Werbung?).

**Sicherheit der gesammelten Standort-/Personendaten**
Eine Datenbank mit Personalien plus Bewegungsmuster (wer war wann wo) ist ein attraktives Ziel für Datenlecks oder Stalking-Missbrauch. Verschlüsselung, Zugriffsprotokollierung und Datensparsamkeit sind hier zentral, nicht optional.

## Empfehlungen für nächste Schritte

- Rechtliche Abklärung zu Datenschutz (Personalien-Erfassung) und Haftung, bevor technisches Design vertieft wird.
- Prüfen, ob ein Verifizierungsschritt (statt reiner Angabe) für Personalien nötig ist, um den Schutzzweck tatsächlich zu erfüllen.
- Moderationsprozess für die "auffällig"-Meldung definieren.
- Polizei-Feature als separates, langfristiges Kooperationsprojekt behandeln statt als Kern-MVP-Feature.
- MVP-Scope reduzieren: Meet erstellen/finden, öffentlich/privat, Zeit-Modi, Basis-Standortfreigabe. Polizei-Integration und Meldefunktion als spätere Phase.