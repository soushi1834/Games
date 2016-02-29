/******************************************************************************
* File Name : main.cpp                           Ver : 1.00  Date : 2014-06-24
*
* Description :
*
*		．
*
* Author : .
******************************************************************************/
#include	"ShootingApplication.h"

/****** リンカオプション *****************************************************/

#pragma comment( linker, "/entry:mainCRTStartup" )
#pragma comment( linker, "/SUBSYSTEM:WINDOWS"    )

/****** メイン関数 *************************************************************/

void main( int argc, char* argv[] )
{
	ShootingApplication application( argc, argv );

	//アプリケーションを実行する
	application.run();
}

/********** End of File ******************************************************/
