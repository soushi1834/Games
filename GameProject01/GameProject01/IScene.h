#ifndef _ISCENE_H_
#define _ISCENE_H_

#include "SceneID.h"
#include "WaveSound.h"

class IScene
{
public:
	IScene() : isEnd_(false), next_(SCENE_NULLSCENE)
	{}
	virtual ~IScene(){}

	virtual void initialize() = 0;

	virtual void update(float time) = 0;

	virtual void draw() = 0;

	virtual bool isEnd() const = 0;

	virtual SceneID nextScene() const = 0;

	virtual void end() const = 0;

protected:
	bool isEnd_;
	SceneID next_;
	WaveSound sound;
};

#endif