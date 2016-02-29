#include "WaveSound.h"

WaveSound::WaveSound()
{
	soundsflag.clear();
}

WaveSound::~WaveSound()
{
	soundsflag.clear();
}

void WaveSound::addSound(const SoundID id, const char* filename, int count)
{
	gsLoadSE(id, filename, count, GWAVE_DEFAULT);
	soundsflag[id] = false;
}

void WaveSound::playSound(const SoundID id)
{
	gsPlaySE(id);
}

void WaveSound::playSoundOnce(const SoundID id)
{
	if (soundsflag[id] == false)
	{
		gsPlaySE(id);
		soundsflag[id] = true;
	}
}

void WaveSound::playSoundLoop(const SoundID id)
{
	if (gsIsPlaySE(id) == FALSE)
		gsPlaySE(id);
}

void WaveSound::stopSound(const SoundID id)
{
	gsStopSE(id);
}

void WaveSound::deleteSound(const SoundID id)
{
	gsDeleteSE(id);
}

bool WaveSound::isPlaying(const SoundID id)
{
	return gsIsPlaySE(id) == TRUE;
}

void WaveSound::soundOnceFlagReset(SoundID id)
{
	soundsflag[id] = false;
}