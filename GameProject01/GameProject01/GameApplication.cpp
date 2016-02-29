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
		//ウインドウの設定
		glutInitDisplayMode( GLUT_DOUBLE | GLUT_RGBA | GLUT_DEPTH );
		glutInitWindowPosition( getWindowPositonX(), getWindowPositonY() );
		glutInitWindowSize( getWindowWidth(), getWindowHeight() );

		//フルスクリーンモードかどうか
		if( isFullScreenMode() == true )
		{
			std::ostringstream mode;
			mode << getWindowWidth() << "x" << getWindowHeight() << "@60";
			//フルスクリーンモードに設定
			glutGameModeString( mode.str().c_str() );
			glutEnterGameMode();
			glutSetCursor( GLUT_CURSOR_NONE );
		}
		else
		{
			//ウインドウモード
			glutCreateWindow( getWindowTitle().c_str() );
		}

		//グラフィックシステムの初期化
		gsInitGraphics();
		//サウンドシステムの初期化
		gsInitSound( (HWND)_glutGetHWND() );
		//入力デバイスの初期化
		gsInitInput( (HWND)_glutGetHWND() );
		//Vsyncを有効に
		setSwapInterval( 1 );
		//乱数の初期化
		gsRandamize();
		//OpenGLの初期化
		initializeGL();
		//アプリケーションの初期化処理
		initialize();
		//終了処理の関数を設定する
		_glutDestroyFunc( destroy );
		//ウインドウサイズ変更時の関数を設定
		glutReshapeFunc( reshape );
		//表示処理の関数を設定
		glutDisplayFunc( display );
		//イベントのない時の関数を設定
		glutIdleFunc( idle );
		//ウインドウがアクティブになった時の関数を設定
		_glutActivateFunc( activate );
		//フレームタイマーのリセット
		gsFrameTimerReset();
		//メインループ処理を実行
		glutMainLoop();
	}

	void GameApplication::initializeGL( void )
	{
		const static float LightAmbient[] = { 0.2f, 0.2f, 0.2f, 0.2f };
		const static float LightDiffuse[] = { 1.0f, 1.0f, 1.0f, 1.0f };
		const static float LightSpeclar[] = { 1.0f, 1.0f, 1.0f, 1.0f };
		const static float LightPosition[] = { 1.0f, 1.0f, 1.0f, 1.0f };

		//画面クリア時のカラーの設定
		glClearColor( 0.0f, 0.0f, 0.0f, 1.0f );
		//面カリングの処理を有効に
		glEnable( GL_CULL_FACE );
		glCullFace( GL_BACK );
		//デプスバッファを1.0でクリアする
		glClearDepth( 1.0 );
		//デプステストを有効にする
		glEnable( GL_DEPTH_TEST );
		//パースペクティブ補正を行う
		glHint( GL_PERSPECTIVE_CORRECTION_HINT, GL_NICEST );
		//ブレンドを有効
		glEnable( GL_BLEND );
		//デフォルトのブレンド式を設定
		glBlendFunc( GL_SRC_ALPHA, GL_ONE_MINUS_SRC_ALPHA );
		//デフォルトの視点変換の変換行列を設定
		glMatrixMode( GL_MODELVIEW );
		glLoadIdentity();
		gluLookAt
			(
				0.0f, 0.0f,10.0f,
				0.0f, 0.0f, 0.0f,
				0.0f, 1.0f, 0.0f
			);
		//デフォルトのライトの設定
		glLightfv( GL_LIGHT0, GL_AMBIENT, LightAmbient );
		glLightfv( GL_LIGHT0, GL_DIFFUSE, LightDiffuse );
		glLightfv( GL_LIGHT0, GL_SPECULAR, LightSpeclar );
		glLightfv( GL_LIGHT0, GL_POSITION, LightPosition );
		glEnable( GL_LIGHT0 );
		//ライティングを有効にする
		glEnable( GL_LIGHTING );
	}

	void GameApplication::destroy( void )
	{
		//ゲーム終了処理
		minstanse -> finish();
		//入力デバイスシステムの終了処理
		gsFinishInput();
		//サウンドシステムの終了処理
		gsFinishSound();
		//グラフィックシステムの終了処理
		gsFinishGraphics();
	}

	void GameApplication::reshape( int width, int height )
	{
		//高さが０にならないように調整
		height = ( height == 0 ) ? 1: height;
		//ビューポートの設定
		glViewport( 0, 0, width, height );
		//透視射影の変換行列を設定
		glMatrixMode( GL_PROJECTION );
		glLoadIdentity();
		gluPerspective
			(
			minstanse -> getPerspectiveFov(),
			(float)width / (float)height,
			minstanse -> getPerspectiveNear(),
			minstanse -> getPerspectiveFar()
			);
		//モデルビューモードにする
		glMatrixMode( GL_MODELVIEW );
		//ウインドウの幅と高さを更新する
		minstanse -> setWindowWidth( width );
		minstanse -> setWindowHeight( height );
	}

	void GameApplication::idle( void )
	{
		//フレームタイマーの更新
		gsFrameTimerUpdate();
		do
		{
			//入力デバイスの読み込み
			gsReadInput();
			//ゲーム更新処理
			minstanse -> update( gsFrameTimerGetTime() );
			//エスケープキーが押されたか
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
				//強制終了する
				std::exit( 0 );
			}
		}
		while( gsFrameTimerIsSkip() == GS_TRUE );
		//描画処理を呼び出す
		glutPostRedisplay();
	}

	void GameApplication::display( void )
	{
		//画面をクリア
		glClear( GL_COLOR_BUFFER_BIT| GL_DEPTH_BUFFER_BIT );
		//ゲーム描画処理
		minstanse -> draw();
		//ダブルバッファの切り替え
		glutSwapBuffers();
		//タイマーウェイトを行う
		gsFrameTimerWait();
	}

	void GameApplication::activate( int state )
	{
		//入力デヴァイスシステムにアクティブ状態を伝える
		gsActivateInput( state );
		//サウンドシステムにアクティブ状態を伝える
		gsActivateSound( state );
	}

	void GameApplication::setSwapInterval( int interval )
	{
		//wglSwapIntervalEXT拡張関数のポインタ関数型
		typedef BOOL ( WINAPI * LPFNWGLSWAPINTERVALEXTPROC )( int interval );
		LPFNWGLSWAPINTERVALEXTPROC wglSwapIntervalEXT = NULL;
		//SwapIntervalEXTの関数ポインタを取得
		wglSwapIntervalEXT = ( LPFNWGLSWAPINTERVALEXTPROC )
			wglGetProcAddress( "wglSwapIntervalEXT" );
		//SwapIntervalEXT関数が取得できたか
		if( wglSwapIntervalEXT != NULL )
		{
			wglSwapIntervalEXT( interval );
		}
	}

	//ウインドウタイトルを取得
	const std::string& GameApplication::getWindowTitle() const
	{
		return mWindowTitle;
	}

	//ウインドウのｘ座標を取得
	int GameApplication::getWindowPositonX() const
	{
		return mWindowPositonX;
	}

	//ウインドウのｙ座標を取得
	int GameApplication::getWindowPositonY() const
	{
		return mWindowPositonY;
	}

	//ウインドウの幅を取得
	int GameApplication::getWindowWidth() const
	{
		return mWindowWidth;
	}

	//ウインドウの高さを取得
	int GameApplication::getWindowHeight() const
	{
		return mWindowHeight;
	}

	//視野角を取得
	float GameApplication::getPerspectiveFov() const
	{
		return mPerspectiveFov;
	}

	//近クリップ面を取得
	float GameApplication::getPerspectiveNear() const
	{
		return mPerspectiveNear;
	}

	//遠クリップ面を取得
	float GameApplication::getPerspectiveFar() const
	{
		return mPerspectiveFar;
	}

	//ウインドウタイトルを設定
	void GameApplication::setWindowTitle( const std::string& title )
	{
		mWindowTitle = title;
	}

	//ウインドウのｘ座標を設定
	void GameApplication::setWindowPositionX( int x )
	{
		mWindowPositonX = x;
	}

	//ウインドウのｙ座標を設定
	void GameApplication::setWindowPositionY( int y )
	{
		mWindowPositonY = y;
	}

	//ウインドウの幅を設定
	void GameApplication::setWindowWidth( int width )
	{
		mWindowWidth = width;
	}

	//ウインドウの高さを設定
	void GameApplication::setWindowHeight( int height )
	{
		mWindowHeight = height;
	}

	//視野角を設定
	void GameApplication::setPerspectiveFov( float fov )
	{
		mPerspectiveFov = fov;
	}

	//近クリップ面を設定
	void GameApplication::setPerspectiveNear( float znear )
	{
		mPerspectiveNear = znear;
	}

	//遠クリップ面を設定
	void GameApplication::setPerspectiveFar( float zfar )
	{
		mPerspectiveFar = zfar;
	}

	//フルスクリーンモードを設定
	void GameApplication::setFullScreenMode( bool mode )
	{
		misFullScreenMode = mode;
	}

	//フルスクリーンモードか調べる
	bool GameApplication::isFullScreenMode()
	{
		return misFullScreenMode;
	}