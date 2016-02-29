#include "GameApplication.h"
#include "Screen.h"
#include <gslib.h>
#include <sstream>

GameApplication* GameApplication::minstanse = 0;

GameApplication::GameApplication( int argc, char* argv[] ):
	mWindowTitle( "SHOOTING" ),
	mWindowPositonX( 0 ),
	mWindowPositonY( 0 ),
	mWindowWidth( SCREEN_WIDTH ),
	mWindowHeight( SCREEN_HEIGHT ),
	misFullScreenMode( true ),
	mPerspectiveFov( 50.0f ),
	mPerspectiveNear( 0.5f ),
	mPerspectiveFar( 100.0f )
	{
		minstanse = this;

		glutInit( &argc, argv );
	}

	void GameApplication::run()
	{
		//�E�C���h�E�̐ݒ�
		glutInitDisplayMode( GLUT_DOUBLE | GLUT_RGBA | GLUT_DEPTH );
		glutInitWindowPosition( getWindowPositonX(), getWindowPositonY() );
		glutInitWindowSize( getWindowWidth(), getWindowHeight() );

		//�t���X�N���[�����[�h���ǂ���
		if( isFullScreenMode() == true )
		{
			std::ostringstream mode;
			mode << getWindowWidth() << "x" << getWindowHeight() << "@60";
			//�t���X�N���[�����[�h�ɐݒ�
			glutGameModeString( mode.str().c_str() );
			glutEnterGameMode();
			glutSetCursor( GLUT_CURSOR_NONE );
		}
		else
		{
			//�E�C���h�E���[�h
			glutCreateWindow( getWindowTitle().c_str() );
		}

		//�O���t�B�b�N�V�X�e���̏�����
		gsInitGraphics();
		//�T�E���h�V�X�e���̏�����
		gsInitSound( (HWND)_glutGetHWND() );
		//���̓f�o�C�X�̏�����
		gsInitInput( (HWND)_glutGetHWND() );
		//Vsync��L����
		setSwapInterval( 1 );
		//�����̏�����
		gsRandamize();
		//OpenGL�̏�����
		initializeGL();
		//�A�v���P�[�V�����̏���������
		initialize();
		//�I�������̊֐���ݒ肷��
		_glutDestroyFunc( destroy );
		//�E�C���h�E�T�C�Y�ύX���̊֐���ݒ�
		glutReshapeFunc( reshape );
		//�\�������̊֐���ݒ�
		glutDisplayFunc( display );
		//�C�x���g�̂Ȃ����̊֐���ݒ�
		glutIdleFunc( idle );
		//�E�C���h�E���A�N�e�B�u�ɂȂ������̊֐���ݒ�
		_glutActivateFunc( activate );
		//�t���[���^�C�}�[�̃��Z�b�g
		gsFrameTimerReset();
		//���C�����[�v���������s
		glutMainLoop();
	}

	void GameApplication::initializeGL( void )
	{
		const static float LightAmbient[] = { 0.2f, 0.2f, 0.2f, 0.2f };
		const static float LightDiffuse[] = { 1.0f, 1.0f, 1.0f, 1.0f };
		const static float LightSpeclar[] = { 1.0f, 1.0f, 1.0f, 1.0f };
		const static float LightPosition[] = { 1.0f, 1.0f, 1.0f, 1.0f };

		//��ʃN���A���̃J���[�̐ݒ�
		glClearColor( 0.0f, 0.0f, 0.0f, 1.0f );
		//�ʃJ�����O�̏�����L����
		glEnable( GL_CULL_FACE );
		glCullFace( GL_BACK );
		//�f�v�X�o�b�t�@��1.0�ŃN���A����
		glClearDepth( 1.0 );
		//�f�v�X�e�X�g��L���ɂ���
		glEnable( GL_DEPTH_TEST );
		//�p�[�X�y�N�e�B�u�␳���s��
		glHint( GL_PERSPECTIVE_CORRECTION_HINT, GL_NICEST );
		//�u�����h��L��
		glEnable( GL_BLEND );
		//�f�t�H���g�̃u�����h����ݒ�
		glBlendFunc( GL_SRC_ALPHA, GL_ONE_MINUS_SRC_ALPHA );
		//�f�t�H���g�̎��_�ϊ��̕ϊ��s���ݒ�
		glMatrixMode( GL_MODELVIEW );
		glLoadIdentity();
		gluLookAt
			(
				0.0f, 0.0f,10.0f,
				0.0f, 0.0f, 0.0f,
				0.0f, 1.0f, 0.0f
			);
		//�f�t�H���g�̃��C�g�̐ݒ�
		glLightfv( GL_LIGHT0, GL_AMBIENT, LightAmbient );
		glLightfv( GL_LIGHT0, GL_DIFFUSE, LightDiffuse );
		glLightfv( GL_LIGHT0, GL_SPECULAR, LightSpeclar );
		glLightfv( GL_LIGHT0, GL_POSITION, LightPosition );
		glEnable( GL_LIGHT0 );
		//���C�e�B���O��L���ɂ���
		glEnable( GL_LIGHTING );
	}

	void GameApplication::destroy( void )
	{
		//�Q�[���I������
		minstanse -> finish();
		//���̓f�o�C�X�V�X�e���̏I������
		gsFinishInput();
		//�T�E���h�V�X�e���̏I������
		gsFinishSound();
		//�O���t�B�b�N�V�X�e���̏I������
		gsFinishGraphics();
	}

	void GameApplication::reshape( int width, int height )
	{
		//�������O�ɂȂ�Ȃ��悤�ɒ���
		height = ( height == 0 ) ? 1: height;
		//�r���[�|�[�g�̐ݒ�
		glViewport( 0, 0, width, height );
		//�����ˉe�̕ϊ��s���ݒ�
		glMatrixMode( GL_PROJECTION );
		glLoadIdentity();
		gluPerspective
			(
			minstanse -> getPerspectiveFov(),
			(float)width / (float)height,
			minstanse -> getPerspectiveNear(),
			minstanse -> getPerspectiveFar()
			);
		//���f���r���[���[�h�ɂ���
		glMatrixMode( GL_MODELVIEW );
		//�E�C���h�E�̕��ƍ������X�V����
		minstanse -> setWindowWidth( width );
		minstanse -> setWindowHeight( height );
	}

	void GameApplication::idle( void )
	{
		//�t���[���^�C�}�[�̍X�V
		gsFrameTimerUpdate();
		do
		{
			//���̓f�o�C�X�̓ǂݍ���
			gsReadInput();
			//�Q�[���X�V����
			minstanse -> update( gsFrameTimerGetTime() );
			//�G�X�P�[�v�L�[�������ꂽ��
			if( GetAsyncKeyState( VK_ESCAPE ) != 0 )
			{
				if( minstanse -> isFullScreenMode() == true )
				{
					glutLeaveGameMode();
				}
				else
				{
					minstanse -> destroy();
				}
				//�����I������
				std::exit( 0 );
			}
		}
		while( gsFrameTimerIsSkip() == GS_TRUE );
		//�`�揈�����Ăяo��
		glutPostRedisplay();
	}

	void GameApplication::display( void )
	{
		//��ʂ��N���A
		glClear( GL_COLOR_BUFFER_BIT| GL_DEPTH_BUFFER_BIT );
		//�Q�[���`�揈��
		minstanse -> draw();
		//�_�u���o�b�t�@�̐؂�ւ�
		glutSwapBuffers();
		//�^�C�}�[�E�F�C�g���s��
		gsFrameTimerWait();
	}

	void GameApplication::activate( int state )
	{
		//���̓f���@�C�X�V�X�e���ɃA�N�e�B�u��Ԃ�`����
		gsActivateInput( state );
		//�T�E���h�V�X�e���ɃA�N�e�B�u��Ԃ�`����
		gsActivateSound( state );
	}

	void GameApplication::setSwapInterval( int interval )
	{
		//wglSwapIntervalEXT�g���֐��̃|�C���^�֐��^
		typedef BOOL ( WINAPI * LPFNWGLSWAPINTERVALEXTPROC )( int interval );
		LPFNWGLSWAPINTERVALEXTPROC wglSwapIntervalEXT = NULL;
		//SwapIntervalEXT�̊֐��|�C���^���擾
		wglSwapIntervalEXT = ( LPFNWGLSWAPINTERVALEXTPROC )
			wglGetProcAddress( "wglSwapIntervalEXT" );
		//SwapIntervalEXT�֐����擾�ł�����
		if( wglSwapIntervalEXT != NULL )
		{
			wglSwapIntervalEXT( interval );
		}
	}

	//�E�C���h�E�^�C�g�����擾
	const std::string& GameApplication::getWindowTitle() const
	{
		return mWindowTitle;
	}

	//�E�C���h�E�̂����W���擾
	int GameApplication::getWindowPositonX() const
	{
		return mWindowPositonX;
	}

	//�E�C���h�E�̂����W���擾
	int GameApplication::getWindowPositonY() const
	{
		return mWindowPositonY;
	}

	//�E�C���h�E�̕����擾
	int GameApplication::getWindowWidth() const
	{
		return mWindowWidth;
	}

	//�E�C���h�E�̍������擾
	int GameApplication::getWindowHeight() const
	{
		return mWindowHeight;
	}

	//����p���擾
	float GameApplication::getPerspectiveFov() const
	{
		return mPerspectiveFov;
	}

	//�߃N���b�v�ʂ��擾
	float GameApplication::getPerspectiveNear() const
	{
		return mPerspectiveNear;
	}

	//���N���b�v�ʂ��擾
	float GameApplication::getPerspectiveFar() const
	{
		return mPerspectiveFar;
	}

	//�E�C���h�E�^�C�g����ݒ�
	void GameApplication::setWindowTitle( const std::string& title )
	{
		mWindowTitle = title;
	}

	//�E�C���h�E�̂����W��ݒ�
	void GameApplication::setWindowPositionX( int x )
	{
		mWindowPositonX = x;
	}

	//�E�C���h�E�̂����W��ݒ�
	void GameApplication::setWindowPositionY( int y )
	{
		mWindowPositonY = y;
	}

	//�E�C���h�E�̕���ݒ�
	void GameApplication::setWindowWidth( int width )
	{
		mWindowWidth = width;
	}

	//�E�C���h�E�̍�����ݒ�
	void GameApplication::setWindowHeight( int height )
	{
		mWindowHeight = height;
	}

	//����p��ݒ�
	void GameApplication::setPerspectiveFov( float fov )
	{
		mPerspectiveFov = fov;
	}

	//�߃N���b�v�ʂ�ݒ�
	void GameApplication::setPerspectiveNear( float znear )
	{
		mPerspectiveNear = znear;
	}

	//���N���b�v�ʂ�ݒ�
	void GameApplication::setPerspectiveFar( float zfar )
	{
		mPerspectiveFar = zfar;
	}

	//�t���X�N���[�����[�h��ݒ�
	void GameApplication::setFullScreenMode( bool mode )
	{
		misFullScreenMode = mode;
	}

	//�t���X�N���[�����[�h�����ׂ�
	bool GameApplication::isFullScreenMode()
	{
		return misFullScreenMode;
	}