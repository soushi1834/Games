#ifndef _GAMEAPPLICATION_H_
#define _GAMEAPPLICATION_H_

#include <string>

class GameApplication
{
public:
	void run();

protected:
	GameApplication( int argc, char* argv[] );
	virtual ~GameApplication(){}
	virtual void initialize(){}
	virtual void update( float frameTimer ){}
	virtual void draw(){}
	virtual void finish(){}
	const std::string& getWindowTitle() const;
	int getWindowPositonX() const;
	int getWindowPositonY() const;
	int getWindowWidth() const;
	int getWindowHeight() const;
	float getPerspectiveFov() const;
	float getPerspectiveNear() const;
	float getPerspectiveFar() const;
	void setWindowTitle( const std::string& title );
	void setWindowPositionX( int x );
	void setWindowPositionY( int y );
	void setWindowWidth( int width );
	void setWindowHeight( int height );
	void setPerspectiveFov( float fov );
	void setPerspectiveNear( float znear );
	void setPerspectiveFar( float zfar );
	void setFullScreenMode( bool mode );
	bool isFullScreenMode();

private:
	void initializeGL( void );
	void setSwapInterval( int interval );

	static void activate( int atate );
	static void display( void );
	static void idle( void );
	static void reshape( int width, int height );
	static void destroy( void );

private:
	std::string mWindowTitle;
	int mWindowPositonX;
	int mWindowPositonY;
	int mWindowWidth;
	int mWindowHeight;
	bool misFullScreenMode;
	float mPerspectiveFov;
	float mPerspectiveNear;
	float mPerspectiveFar;

	//staticƒƒ“ƒoŠÖ”‚Ístaticƒƒ“ƒo•Ï”‚µ‚©QÆ‚Å‚«‚È‚¢
	static GameApplication* minstanse;
};

#endif