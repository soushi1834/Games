#include "NullScene.h"

NullScene::NullScene()
{

}

NullScene::~NullScene()
{}

void NullScene::initialize()
{

}

void NullScene::update(float time)
{

}

void NullScene::draw()
{

}

bool NullScene::isEnd() const
{
	return false;
}

SceneID NullScene::nextScene() const
{
	return SCENE_NULLSCENE;
}

void NullScene::end() const
{

}