PROJECT_PATH="/data/apps/GAMEEngine"
LIB_PATH="/data/apps/GAMEEngine/lib"
GBA_LIB_PATH="$PROJECT_PATH/build"
GBA_LIB_PATH=$GBA_LIB_PATH:$LIB_PATH/mysql-connector-java-5.1.7-bin.jar
GBA_LIB_PATH=$GBA_LIB_PATH:$LIB_PATH/log4j-1.2.9.jar
GBA_LIB_PATH=$GBA_LIB_PATH:$LIB_PATH/c3p0-0.9.1.2.jar
echo $GBA_LIB_PATHr
java -cp $GBA_LIB_PATH com.ts.dt.Main

