PROJECT_PATH="/data/apps/GAMEEngine"
LIB_PATH="/data/apps/GAMEEngine/lib"
GBA_LIB_PATH="$PROJECT_PATH/build:$LIB_PATH/jpersist_jwebapp.jar:$LIB_PATH/mysql-connector-java-5.1.7-bin.jar:$LIB_PATH/log4j-1.2.9.jar:$LIB_PATH/DBPool-4.9.3.jar"
GBA_LIB_PATH=$GBA_LIB_PATH:$LIB_PATH/antlr-2.7.6.jar
GBA_LIB_PATH=$GBA_LIB_PATH:$LIB_PATH/commons-collections-3.1.jar
GBA_LIB_PATH=$GBA_LIB_PATH:$LIB_PATH/dom4j-1.6.1.jar
GBA_LIB_PATH=$GBA_LIB_PATH:$LIB_PATH/hibernate3.jar
GBA_LIB_PATH=$GBA_LIB_PATH:$LIB_PATH/javassist-3.9.0.GA.jar
GBA_LIB_PATH=$GBA_LIB_PATH:$LIB_PATH/jta-1.1.jar
GBA_LIB_PATH=$GBA_LIB_PATH:$LIB_PATH/slf4j-api-1.5.8.jar
GBA_LIB_PATH=$GBA_LIB_PATH:$LIB_PATH/slf4j-nop-1.5.2.jar
echo $GBA_LIB_PATHr
java -cp $GBA_LIB_PATH com.ts.dt.Main

