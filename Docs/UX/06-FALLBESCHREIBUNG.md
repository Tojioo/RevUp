# 1. Fallbeschreibung: RevUp

*Dokument 6 von 11 · [← 05 Icon-Konzept](../Design/05-gauge-mark.html) · Weiter: [07 Ziele →](07-ZIELE.md)*

## Fall

Car- und Bike-Meets werden heute meist informell über Chatgruppen oder soziale Medien organisiert. Der Standort wird oft öffentlich geteilt, bevor der Veranstalter weiss, wer teilnimmt. Das führt zu ungebetenen Gästen, Lärmbelästigung und Ärger mit Anwohnern. Es fehlt eine strukturierte Möglichkeit, ein Meet nach Thema, Zeitrahmen und Teilnehmerzahl zu organisieren und dabei zu steuern, wer den Treffpunkt sehen darf.

RevUp soll das lösen: Veranstalter erstellen Meets mit klaren Rahmenbedingungen (Thema, Zeit, Limits), Teilnehmer melden sich an, der Standort wird erst nach Angabe von Kontaktdaten (öffentliche Meets) bzw. nach Freigabe durch den Veranstalter (private Meets) sichtbar.

## Fragenliste

- Muss ein Meet an einen festen Ort gebunden sein, oder kann sich der Treffpunkt kurzfristig ändern?
- Reicht eine Namensangabe zur "Kontaktdaten"-Hürde, oder wird mehr erwartet (siehe Datenschutz-Diskussion in `03-KONZEPT.md`)?
- Wie erfährt ein Teilnehmer, dass eine private Anfrage angenommen/abgelehnt wurde (Push, E-Mail, nur beim nächsten App-Besuch)?
- Kann ein Veranstalter ein laufendes Meet noch ändern (z. B. Ort verschieben), nachdem Teilnehmer bereits zugesagt haben?
- Was passiert, wenn die maximale Teilnehmerzahl erreicht ist? Warteliste oder harte Grenze?
- Braucht ein Meet einen Titel-Zwang zur Eindeutigkeit, oder dürfen sich Titel wiederholen?
- Ist ein Meet nach Ablauf des Zeitfensters weiterhin sichtbar (Archiv), oder verschwindet es sofort?
- Wie wird zwischen privatem und gewerblichem Veranstalter unterschieden? Reicht ein Flag, oder braucht es eine echte Verifizierung (aus MVP-Sicht: nein, siehe Backlog)?

Diese Liste ergänzt die bereits in `03-KONZEPT.md` (Abschnitt "Kritische Punkte und offene Fragen") dokumentierten grösseren, teils rechtlichen Fragestellungen.