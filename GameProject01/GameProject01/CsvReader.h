#ifndef _CSV_READER_
#define _CSV_READER_

#include <vector>
#include <string>

class CsvReader
{
public:
	CsvReader();

	~CsvReader();
	
	//データの読み込み
	bool load(const std::string& fileName);
	
	//データの行を取得
	int getRows() const;
	
	//データの列を取得
	int getColumns(int row = 0) const;
	
	//データを文字列で取得
	const std::string& getDataToString(int row, int col) const;
	
	//データをintで取得
	int getDataToInteger(int row, int col) const;
	
	//データをfloatで取得
	float getDataToFloat(int row, int col) const;
	
	//データをdoubleで取得
	double getDataToDouble(int row, int col) const;

private:
	typedef std::vector<std::string> Row;	//文字列データコンテナ行
	typedef std::vector<Row> Rows;			//文字列データ１行コンテナ型
	Rows mData;								//データ格納コンテナ
};

#endif