#ifndef _CSV_READER_
#define _CSV_READER_

#include <vector>
#include <string>

class CsvReader
{
public:
	CsvReader();

	~CsvReader();
	
	//�f�[�^�̓ǂݍ���
	bool load(const std::string& fileName);
	
	//�f�[�^�̍s���擾
	int getRows() const;
	
	//�f�[�^�̗���擾
	int getColumns(int row = 0) const;
	
	//�f�[�^�𕶎���Ŏ擾
	const std::string& getDataToString(int row, int col) const;
	
	//�f�[�^��int�Ŏ擾
	int getDataToInteger(int row, int col) const;
	
	//�f�[�^��float�Ŏ擾
	float getDataToFloat(int row, int col) const;
	
	//�f�[�^��double�Ŏ擾
	double getDataToDouble(int row, int col) const;

private:
	typedef std::vector<std::string> Row;	//������f�[�^�R���e�i�s
	typedef std::vector<Row> Rows;			//������f�[�^�P�s�R���e�i�^
	Rows mData;								//�f�[�^�i�[�R���e�i
};

#endif