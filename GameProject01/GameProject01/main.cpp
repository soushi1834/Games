/******************************************************************************
* File Name : main.cpp                           Ver : 1.00  Date : 2014-06-24
*
* Description :
*
*		�D
*
* Author : .
******************************************************************************/
#include	"ShootingApplication.h"

/****** �����J�I�v�V���� *****************************************************/

#pragma comment( linker, "/entry:mainCRTStartup" )
#pragma comment( linker, "/SUBSYSTEM:WINDOWS"    )

/****** ���C���֐� *************************************************************/

void main( int argc, char* argv[] )
{
	ShootingApplication application( argc, argv );

	//�A�v���P�[�V���������s����
	application.run();
}

/********** End of File ******************************************************/
