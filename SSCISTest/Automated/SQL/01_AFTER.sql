-- TS.01
-- TC.01.00
DELETE SSCIS.SSCIS_SESSION WHERE ID_USER = (SELECT ID FROM SSCIS.SSCIS_USER WHERE LOGIN = 'USER_TST_00');
DELETE SSCIS.SSCIS_USER WHERE LOGIN = 'USER_TST_00';

-- TC.01.01

-- TC.01.02
DELETE SSCIS.TUTOR_APPLICATION_SUBJECT WHERE ID_APPLICATION = (SELECT ID FROM SSCIS.TUTOR_APPLICATION WHERE ID_USER = (SELECT ID FROM SSCIS.SSCIS_USER WHERE LOGIN = 'USER_TST_01'));
DELETE SSCIS.TUTOR_APPLICATION WHERE ID_USER = (SELECT ID FROM SSCIS.SSCIS_USER WHERE LOGIN = 'USER_TST_01');
DELETE SSCIS.SSCIS_SESSION WHERE ID_USER = (SELECT ID FROM SSCIS.SSCIS_USER WHERE LOGIN = 'USER_TST_01');
DELETE SSCIS.SSCIS_USER WHERE LOGIN = 'USER_TST_01';

-- TC.01.03
DELETE SSCIS.SSCIS_SESSION WHERE ID_USER = (SELECT ID FROM SSCIS.SSCIS_USER WHERE LOGIN = 'ADMIN_TST_00');
DELETE SSCIS.SSCIS_USER WHERE LOGIN = 'ADMIN_TST_00';


COMMIT;
