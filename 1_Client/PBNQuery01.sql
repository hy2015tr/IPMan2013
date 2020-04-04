

SELECT * FROM IPMAN2013.dbo.TableRequest;
SELECT * FROM IPMAN2013.dbo.TableServer;
SELECT * FROM IPMAN2013.dbo.TableSubnet;
SELECT * FROM IPMAN2013.dbo.TableGroup;
SELECT * FROM IPMAN2013.dbo.TableUser;


/*----------------------------------------------------------------------/*

	--- RESET DATABASE -----

    ALTER TABLE IPMAN2013.dbo.TableUser DROP CONSTRAINT [FK_TableUser_TableGroup];
	TRUNCATE TABLE IPMAN2013.dbo.TableRequest;
	TRUNCATE TABLE IPMAN2013.dbo.TableGroup;
	TRUNCATE TABLE IPMAN2013.dbo.TableServer;
	TRUNCATE TABLE IPMAN2013.dbo.TableSubnet;
	TRUNCATE TABLE IPMAN2013.dbo.TableUser;
    ALTER TABLE IPMAN2013.dbo.TableUser ADD CONSTRAINT [FK_TableUser_TableGroup] FOREIGN KEY([GroupID]) REFERENCES IPMAN2013.dbo.TableGroup;


	--- GROUPS ---
	
	INSERT INTO IPMAN2013.dbo.TableGroup VALUES ( 'IT'      , 'IT@AVEA.COM.TR'      , 'IT GROUP'      , 1 );
	INSERT INTO IPMAN2013.dbo.TableGroup VALUES ( 'PBN'     , 'PBN@AVEA.COM.TR'     , 'PBN GROUP'     , 1 );
	INSERT INTO IPMAN2013.dbo.TableGroup VALUES ( 'CABLING' , 'CABLING@AVEA.COM.TR' , 'CABLING GROUP' , 1 );


	--- USERS ---
	
	INSERT INTO IPMAN2013.dbo.TableUser VALUES ( 'HASAN'  , '1' , 'HASAN YILDIRIM' , 'HASAN.YILDIRIM@AVEA.COM.TR' , '1234567' , 1 , 1 , 1 );
	INSERT INTO IPMAN2013.dbo.TableUser VALUES ( 'KORAL'  , '1' , 'KORAL OZGUNAY'  , 'KORAL.OZGUNAY@AVEA.COM.TR'  , '1234567' , 2 , 1 , 1 );
	INSERT INTO IPMAN2013.dbo.TableUser VALUES ( 'CHAABO' , '1' , 'MOHAMAD CHAABO' , 'MOHAMAD.CHAABO@AVEA.COM.TR' , '1234567' , 2 , 1 , 1 );
	INSERT INTO IPMAN2013.dbo.TableUser VALUES ( 'FURKAN' , '1' , 'FURKAN ACIK'    , 'FURKAN.ACIK@AVEA.COM.TR'    , '1234567' , 2 , 1 , 1 );


	--- SUBNET ---
	
	INSERT INTO IPMAN2013.dbo.TableSubNet VALUES ( 'UMRANIYE-IT' , 'UMRANIYE LOKASYON', 1 );
	INSERT INTO IPMAN2013.dbo.TableSubNet VALUES ( 'GUNESLI'     , 'GUNESLI  LOKASYON', 1 );


	--- SERVER ---

	INSERT INTO IPMAN2013.dbo.TableServer VALUES  ('INFOBLOX' , 'HTTPS://10.1.1.104' , 'INFOBLOX WAPI SERVER' , 'webapi', '123456', 1 );

*/
