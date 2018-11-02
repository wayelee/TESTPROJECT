#include "CTestCoordTrans.h"
#include "../mlRVML/mlCoordTrans.h"

CPPUNIT_TEST_SUITE_NAMED_REGISTRATION( CTestCoordTrans,"alltest" );

CTestCoordTrans::CTestCoordTrans()
{
    //ctor
}

CTestCoordTrans::~CTestCoordTrans()
{
    //dtor
}
/** @brief TestmlCoordTrans
  *
  * @todo: document this function
  */
void CTestCoordTrans::TestmlCoordTrans()
{
    CmlCoordTrans cls;
    bool result = cls.mlCoordTrans("","");
    CPPUNIT_ASSERT(result == true);
}

/** @brief TearDown
  *
  * @todo: document this function
  */
void CTestCoordTrans::tearDown()
{

}

/** @brief SetUp
  *
  * @todo: document this function
  */
void CTestCoordTrans::setUp()
{

}

