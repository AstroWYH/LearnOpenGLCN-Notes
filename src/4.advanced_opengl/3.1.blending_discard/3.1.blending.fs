#version 330 core
out vec4 FragColor;

in vec2 TexCoords;

uniform sampler2D texture1;

void main()
{             
    vec4 texColor = texture(texture1, TexCoords); // wyh texColor.a 读取的纹理的.a
    if(texColor.a < 0.1) // wyh unity shader透明度测试, 透明度不满足阈值要求, 直接丢弃
        discard; // wyh 箱子、地面使用shader的时候不受影响, 那是因为它们的.a没有这个阈值
    FragColor = texColor; // wyh 注: 这里要将RGBA 4个都传过去了
}