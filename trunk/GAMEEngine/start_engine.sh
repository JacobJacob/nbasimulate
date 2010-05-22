PROJECT_PATH="/data/apps/GAMEEngine"
LIB_PATH="/data/apps/GAMEEngine/lib"
GBA_LIB_PATH="$PROJECT_PATH/build:$LIB_PATH/jpersist_jwebapp.jar:$LIB_PATH/mysql-connector-java-5.1.7-bin.jar:$LIB_PATH/log4j-1.2.9.jar:$LIB_PATH/DBPool-4.9.3.jar"
echo $GBA_LIB_PATHr
java -cp $GBA_LIB_PATH com.ts.dt.Main

