<<<<<<< HEAD
=======
## LearnOpenGLCN课程学习与代码注释--Astro WANG
[说明] 执行该脚本可获取Learning OpenGL配置完整工程代码

```
>>>>>>> 977fde2937ba4c279cc7f2587ce1a41cde8c3183
#!/bin/sh

# This script will download the dependencies for the Learning
# OpenGL repository and compiles the source code once the
# dependencies have been installed in the install directory. The
# install directory is `../install` relative to this file.
#
# This downloads and installs
#
# - glfw
# - glm
# - assimp
# - learning opengl

d=${PWD}
bd=${d}/../
build_dir=${d}/build.unix
id=${bd}/install/

if [ ! -d ${build_dir} ] ; then
    mkdir ${build_dir}
fi

if [ ! -d ${id} ] ; then
    mkdir ${id}
fi

# GLFW
if [ ! -d ${build_dir}/glfw ] ; then
    mkdir ${build_dir}/glfw
    cd ${build_dir}/glfw
    git clone --depth 1 git@github.com:glfw/glfw.git .
    cd glfw
    git checkout 53c8c72c676ca97c10aedfe3d0eb4271c5b23dba
fi
if [ ! -d ${build_dir}/glfw/build ] ; then 
    mkdir ${build_dir}/glfw/build
fi

cd ${build_dir}/glfw/build
cmake -DGLFW_BUILD_TESTS=Off \
      -DGLFW_BUILD_DOCS=Off \
      -DGLFW_BUILD_EXAMPLES=Off \
      -DCMAKE_INSTALL_PREFIX=${id} \
      ..

if [ $? -ne 0 ] ; then
    echo "Failed to configure GLFW"
    exit
fi

cmake --build . --target install -- -j8
if [ $? -ne 0 ] ; then
    echo "Failed to build GLFW"
    exit
fi

# GLM
if [ ! -d ${build_dir}/glm ] ; then
    cd ${build_dir}
    git clone --depth 1 git@github.com:g-truc/glm.git
    cd glm
    git checkout ddebaba03308475b1e33670267eadd596d039949
fi

if [ ! -d ${build_dir}/glm/build ] ; then
    mkdir ${build_dir}/glm/build
fi

cd ${build_dir}/glm/build
cmake -DCMAKE_INSTALL_PREFIX=${id} \
      -DGLM_TEST_ENABLE=Off \
      ..

if [ $? -ne 0 ] ; then
    echo "Failed to configure GLM"
    exit
fi

cmake --build . --target install
if [ $? -ne 0 ] ; then
    echo :"Failed to install and build GLM"
    exit
fi

# ASSIMP
if [ ! -d ${build_dir}/assimp ] ; then
    cd ${build_dir}
    git clone --depth 1 git@github.com:assimp/assimp.git
    cd assimp
    git checkout 3fbe9095d157ecacf6a9549c9a21bf2ad3110ac6
fi

if [ ! -d ${build_dir}/assimp/build ] ; then
    mkdir ${build_dir}/assimp/build
fi

cd ${build_dir}/assimp/build
cmake -DCMAKE_INSTALL_PREFIX=${id} \
      -DASSIMP_BUILD_TESTS=Off \
      -DASSIMP_INSTALL_PDB=Off \
      ..
if [ $? -ne 0 ] ; then
    echo "Failed to configure assimp."
    exit
fi

cmake --build . --target install -- -j8
if [ $? -ne 0 ] ; then
    echo "Failed to build and install assimp"
    exit
fi

# Learning GL
if [ ! -d ${build_dir}/learninggl ] ; then
    cd ${build_dir}
    git clone --depth 1 git@github.com:JoeyDeVries/LearnOpenGL.git learninggl
    cd learninggl
    git checkout e2d82e2cfba74d8ed3cc2ee40f71b2bc93867eae
fi

cd ${build_dir}/learninggl
cmake -DCMAKE_PREFIX_PATH=${id} \
      -DCMAKE_INSTALL_PREFIX=${id} \
      ../../../

if [ $? -ne 0 ] ; then
    echo "Failed to configure PBR."
    exit
fi

cmake --build . -- -j8
if [ $? -ne 0 ] ; then
    echo "Failed to build PBR."
    exit
fi
<<<<<<< HEAD
=======
```
>>>>>>> 977fde2937ba4c279cc7f2587ce1a41cde8c3183
