#include "EnemyBulletGenerator.h"
#include "EnemyBullet.h"
#include "IWorld.h"
#include "MathUtility.h"
#include "EnemyBulletID.h"

EnemyBulletGenerator::EnemyBulletGenerator(IWorld& world) :
world_(world),
count(0)
{}

void EnemyBulletGenerator::fire(const GSvector2& position, const int bullet_type, const float speed)
{
	GSvector2 v;
	float angle;

	switch (bullet_type)
	{
	case �΂�܂�:
		v = GSvector2(gsRandf(-1, 1), gsRandf(-1, 1));
		v.normalize();
		world_.add(CHARACTER_ENEMY_BULLET, std::make_shared<EnemyBullet>(
			world_,
			position,
			v * speed));
		break;

	case ���@�_���e:
		angle = MathUtility::subtractAngle(position, 0, world_.getPlayer()->getPosition());
		v = GSvector2(
			MathUtility::cos(angle),
			MathUtility::sin(angle)
			);
		v.normalize();
		world_.add(CHARACTER_ENEMY_BULLET, std::make_shared<EnemyBullet>(
			world_,
			position,
			v * speed));
		break;

	case ���@�_���Q�S����:
		angle = MathUtility::subtractAngle(position, 0, world_.getPlayer()->getPosition());
		for (int i = 0; i < 360; i += 15)
		{
			v = GSvector2(
				MathUtility::cos(i + angle),
				MathUtility::sin(i + angle)
				);
			v.normalize();
			world_.add(CHARACTER_ENEMY_BULLET, std::make_shared<EnemyBullet>(
				world_,
				position,
				v * speed));
		}
		break;

	case �Q�S����:
		for (int i = 0; i < 360; i += 15)
		{
			v = GSvector2(
				MathUtility::cos(i),
				MathUtility::sin(i)
				);
			v.normalize();
			world_.add(CHARACTER_ENEMY_BULLET, std::make_shared<EnemyBullet>(
				world_,
				position,
				v * speed));
		}
		break;

	case �O���T����:
		for (int i = 240; i <= 300; i += 15)
		{
			v = GSvector2(
				MathUtility::cos(i),
				MathUtility::sin(i)
				);
			v.normalize();
			world_.add(CHARACTER_ENEMY_BULLET, std::make_shared<EnemyBullet>(
				world_,
				position,
				v * speed));
		}
		break;

	case ���@�_���O���T����:
		angle = MathUtility::subtractAngle(position, 0, world_.getPlayer()->getPosition());
		for (int i = -30; i <= 30; i += 15)
		{
			v = GSvector2(
				MathUtility::cos(i + angle),
				MathUtility::sin(i + angle)
				);
			v.normalize();
			world_.add(CHARACTER_ENEMY_BULLET, std::make_shared<EnemyBullet>(
				world_,
				position,
				v * speed));
		}
		break;

	case �����e:
		for (int i = speed / 2; i <= speed * 2; i++)
		{
			v = GSvector2(0, 1);
			world_.add(CHARACTER_ENEMY_BULLET, std::make_shared<EnemyBullet>(
				world_,
				position,
				v * (i / 2.0f)));
		}

	default:
		break;
	}

	world_.playSE(SE_SHOT);
	count += 1;
}