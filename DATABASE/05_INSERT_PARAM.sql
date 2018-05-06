INSERT INTO SSCIS.SSCIS_PARAM (PARAM_KEY, PARAM_VALUE, DESCRIPTION) VALUES ('SESSION_LENGTH', '900', 'Delka session v sekundach');
INSERT INTO SSCIS.SSCIS_PARAM (PARAM_KEY, PARAM_VALUE, DESCRIPTION) VALUES ('VERSION', '0.0.4', 'Oznaceni verze');
INSERT INTO SSCIS.SSCIS_PARAM (PARAM_KEY, PARAM_VALUE, DESCRIPTION) VALUES ('MAP_TOKEN', 'AIzaSyDO75rUtMMJ0J_SDAPsyF85thwNxL6Mh8I', 'GoogleMaps token');
INSERT INTO SSCIS.SSCIS_PARAM (PARAM_KEY, PARAM_VALUE, DESCRIPTION) VALUES ('MAX_SUBJECTS_COUNT', '10', 'Maximalni pocet predmetu na prihlasce');
COMMIT;


INSERT INTO SSCIS.ENUM_ROLE (ROLE, DESCRIPTION) VALUES ('USER', 'Prihlaseny uzivatel');
INSERT INTO SSCIS.ENUM_ROLE (ROLE, DESCRIPTION) VALUES ('ADMIN', 'Spravce systemu');
INSERT INTO SSCIS.ENUM_ROLE (ROLE, DESCRIPTION) VALUES ('TUTOR', 'Tutor');
COMMIT;

