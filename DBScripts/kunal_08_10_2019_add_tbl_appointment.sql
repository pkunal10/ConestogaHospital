﻿CREATE TABLE [dbo].[Appointments] (
    [AppointmentId] [int] NOT NULL IDENTITY,
    [PatientNo] [nvarchar](max),
    [DoctorId] [int] NOT NULL,
    [AppointmentDate] [datetime] NOT NULL,
    [StartTime] [datetime] NOT NULL,
    [EndTime] [datetime] NOT NULL,
    CONSTRAINT [PK_dbo.Appointments] PRIMARY KEY ([AppointmentId])
)
INSERT [dbo].[__MigrationHistory]([MigrationId], [ContextKey], [Model], [ProductVersion])
VALUES (N'201910081337355_kunal-08_10_2019_add_tbl_appointment', N'ConestogaHospital.Migrations.Configuration',  0x1F8B0800000000000400CD5BCB6EE33614DD17E83F085AB5C08C9564366D60CF20E38C3B419D07A2CC2CBA6324DA212A51AA44A5F6B775D14FEA2FF4522F52D45B96EC201B9BE63DBCBC0F8AF7EAE4BF7FFE9D7FDAB98EF68A83907874A19FCFCE740D53CBB309DD2EF4886DDEFFA27FFAF8E30FF32FB6BBD3BE67F33EF0792049C385FEC2987F6918A1F5825D14CE5C62055EE86DD8CCF25C03D99E717176F6AB717E6E6080D0014BD3E68F1165C4C5F117F8BAF4A8857D1621E7D6B3B113A6E3F08B19A36A77C8C5A18F2CBCD0612E0E99B7455FBDD0270C39B34446D7AE1C82401F133B1B5D43947A0C31D0F6F25B884D1678746BFA30809CA7BD8F61DE0639214E777129A677DDD0D905DF90210433282B02F5DC9E80E71F520B19AAF8203BEBB905C1865FC0D66CCF771DDB71A15FD9768043B098BAD6E5D209F8BC7A2BCF52D9775A69C6BB3C3C208AF81FCC891C1605784171C4023EE3217A7688F53BDE3F797F62BAA091E3C8AA82B2F05B6100861E02CFC701DB3FE24D710337B6AE1945714395CFA5CBA2C94E6F28FB70A16B77A00A7A76701E1792554CE605F8374C718018B61F10633800B7DED838B66C490965C935A1F83C5B0E2211524BD76ED16E8DE996BD2C74F8A86B2BB2C3763692AAF08D12C84410624184DB5659C69A4CBC087C7D2590AB932FF407F1973079E475E686C885E60CF17D8F50E68283876589903F71A6084506654B51FC581903020466DE7993C7D9B56781AAED7B6B4691CC740D1BCEC0F8E72778CCF5C633190A58227920D2176A0FC0E99C2789F906A548227ADAEC10DEEF9B185DE366BC9C805BCCC1716AFAD8820B10AC7628D2A3E7B9A3A767E7B07BF41C3C28E8B8E069438E6B3024E032B963851B5F8F7F3A958B45A80E72B4103FADBB8B19D7D7E97DF2753CD78B554F1900FCBC1BE47A2E785AA76747755F77773BE2C77DA44CE0E2F242ABA3ACB23ECA2ADD0EE2965B998B88234026D3F5D67B26CEF426E95C53B7DDFAC3F06F2F98DE2CD7F79F0FBE58DFB8687BDCB3114E348608E47AAA81FDCC47F08E7DBD352B0E4A48EEF4AC0CD3B58AFB49404DCC8A4EC4A1AE092D926EDA2C6F18192D18A20EAA8491ABEA16A8E4825D8592D51C2D003C4FABC493BB638B70FE08AC8490EF262D40FC88AD82489E6E55CECFDD2C9AA046D205CDBAA5464DBB747E8B7C1FE2506A9FA6239A99F64EDF9BFDDB896E82615861455731D7365F097C0399A1FCCA1FD7365E9120E405327A463C1396B65B9A5608EA1AD3664BA971AB3E9885D13309FE39ED96B5F437553061CB156C8F4770BC539C2B25BAAA25C9B88D8D1C14D41F9B4BCF895CDAD2DC6CC24A7B8C324E3AD41D23E920CA10CB8A106F4210ED4119458C7647CAFB7F32503E58C6991B8A7BD460304AD1A05CCED4F0EA167CF281374EFC4967E480186C92AE8DC36293AF108B4DEDC3C648101DBC422888E1EE58A2D72243D5F56E3AEE34E9D3D5EC35F9B13BAED4AF9311A5E1EE5879C74E46CA07DF4CE467CFE751823E7DA4F78FF73AC1E94329ABD5649CEABAAF31700A057E21761A1A074D8859634EC6CAC6DE4CF02477B3514227BECEF50F9C6AB17AA326355FD1A8558DBC3694A444557192D137E31EF9F63B8A93A40B737F5735091F2FAFD4A65835E61B7364527D8CE2C2B860E9EFBC6AB1690F55D1D65271EADC5387B42AC3ACFA62ACCB18EBBE18E39C3F79D7A970B3C806BBE3645D2519261BEB71171BB1FA119DA3E265331BED71D7E4ADA1C2DD800F74974FDB4232423A74E443A1D44E50A7E4ABE76D05A57D304F4BF92E942CA5B64FA6E85A5CF9D9BCAE37F721C3EE8C4F98997F394B87C44DA06CC22DA26403074FD277D72FCECE2F143ED7DBE1561961683BDD085603DE1D8C496D22DCC4ADEF087A363E0B6C26FA8A02EB05053FB968F7B30CD597B1741090CA4A3A084C611EF5C01AC42E1A122213707A260995128DE720C7A8948B58E571883A367C66A31075062329449D8E387D893A03A26D3C8ECC2431567C673A2426AA5EB20FC1295262A63837CA45EB31392793B84FA5994C61B6BA02F2540C8E490C594DDA98C29CE592EE98AC88C90E9181662B83AD46435A8F865491C98751160ED2A6484B3808AAFACE7B20F1E0B06B8A20170CBE0C14C805A3E5B04A2028BF92AD692A29554D033F2029FCF86B5CFE204CD4948805DDE9036DEC81CA850AEC834EFC82067A41D50A3929A103F7A0967A50059C9215BAB212F62DA484AA25F2DF498795127A420D6BA10A3DE5390C2434943B0D7343FE17B1F9350EC95640F07F18A3D8E225BC00CDE6DCD08D97E513EC48D6289BA29E479821C855741530B24116839F2D88D898D2F31D39517CFC3D63FB86DE47CC8FD81544B3FBEC145E8DCF8DE6F563D64651E7F9BDCFBF85636C01D424FCB8B9A79F23E2D8B9DEAB8A9AA106821F07E93315B432197FB66EF739D29D473B02A5E6BBC63EA6FC89FC845DDF01B0F09E9AE8150FD10DC26B8DB7C8DA670DA37A90764714CD3EBF26681B20374C31843C7C8518B6DDDDC7FF01538A226F29390000 , N'6.2.0-61023')
