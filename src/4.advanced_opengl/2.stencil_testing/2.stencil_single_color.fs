#version 330 core
out vec4 FragColor;

void main()
{
    FragColor = vec4(0.04, 0.28, 0.26, 1.0); // wyh 准备给物体边框着色的颜色
}
// wyh 只有fs, 没有vs, 这个怎么作用到物体边框?应该是外面定义2个不同的shader?是的, shaderSingleColor只用来渲染边框的颜色(draw之前), 并且跟箱子地面的shader共用vs