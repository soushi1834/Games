#include "SceneManager.h"
#include "NullScene.h"

SceneManager::SceneManager() :
mScene(new NullScene())
{
	scenes.clear();
}

SceneManager::~SceneManager()
{}

void SceneManager::initialize()
{
	scenes.clear();
}

void SceneManager::update(float time)
{
	mScene->update(time);
	if (mScene->isEnd())
	{
		changeScene(mScene->nextScene());
	}
}

void SceneManager::draw()
{
	mScene->draw();
}

void SceneManager::changeScene(SceneID id)
{
	mScene->end();
	mScene = scenes[id];
	mScene->initialize();
}

void SceneManager::exit()
{
	mScene->end();
}

void SceneManager::add(SceneID id, IScene* scene)
{
	scenes[id] = ScenePtr(scene);
}