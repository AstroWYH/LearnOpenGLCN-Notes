#version 330 core
layout (location = 0) in vec2 aPos;
layout (location = 1) in vec3 aColor;
layout (location = 2) in vec2 aOffset; // wyh 把这个offset也放在里面

out vec3 fColor;

void main()
{
    fColor = aColor;
    gl_Position = vec4(aPos + aOffset, 0.0, 1.0); // wyh 核心: 100个四边形的偏移, 由这个位置加上加在顶点属性的偏差来得到, 而不是通过来获得调用100次glDrawArrays来实现, 然后提高性能
}