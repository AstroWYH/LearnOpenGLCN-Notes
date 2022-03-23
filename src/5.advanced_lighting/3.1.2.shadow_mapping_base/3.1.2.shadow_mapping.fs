#version 330 core
out vec4 FragColor;

in VS_OUT {
    vec3 FragPos;
    vec3 Normal;
    vec2 TexCoords;
    vec4 FragPosLightSpace;
} fs_in;

uniform sampler2D diffuseTexture; // wyh
uniform sampler2D shadowMap; // wyh 阴影又是一张纹理

uniform vec3 lightPos;
uniform vec3 viewPos;

float ShadowCalculation(vec4 fragPosLightSpace) // wyh 就是计算下是不是在阴影中
{
    // perform perspective divide
    vec3 projCoords = fragPosLightSpace.xyz / fragPosLightSpace.w; // wyh 光源空间的坐标(光源为照相机的裁剪空间); 这里要自己进行透视除法, 教程好像说了
    // transform to [0,1] range
    projCoords = projCoords * 0.5 + 0.5; // wyh 还要换到[0, 1]空间
    // get closest depth value from light's perspective (using [0,1] range fragPosLight as coords)
    float closestDepth = texture(shadowMap, projCoords.xy).r; // wyh 非常核心: 采样阴影纹理, 就是光源看到的距离; 这里是采样到的具体位置, 那就应该是比较短的最面前的距离
    // get depth of current fragment from light's perspective
    float currentDepth = projCoords.z; // wyh 核心: 就是照相机看到的距离; 但这公式好像不是这么回事, 有点尴尬; 好像没错, 这个.z反映的应该是远的距离; 不懂, 记下来吧！！！
    // check whether current frag pos is in shadow
    float shadow = currentDepth > closestDepth  ? 1.0 : 0.0; // wyh 核心: 阴影说白了就是个比距离大小; 观察位置看到的 > 光源位置看到的就是阴影

    return shadow;
}

void main()
{           
    vec3 color = texture(diffuseTexture, fs_in.TexCoords).rgb; // wyh 采样漫反射贴图, 就是普通的纹理
    vec3 normal = normalize(fs_in.Normal);
    vec3 lightColor = vec3(0.3);
    // ambient
    vec3 ambient = 0.3 * lightColor;
    // diffuse
    vec3 lightDir = normalize(lightPos - fs_in.FragPos);
    float diff = max(dot(lightDir, normal), 0.0);
    vec3 diffuse = diff * lightColor;
    // specular
    vec3 viewDir = normalize(viewPos - fs_in.FragPos);
    vec3 reflectDir = reflect(-lightDir, normal);
    float spec = 0.0;
    vec3 halfwayDir = normalize(lightDir + viewDir);  
    spec = pow(max(dot(normal, halfwayDir), 0.0), 64.0);
    vec3 specular = spec * lightColor;    
    // calculate shadow
    float shadow = ShadowCalculation(fs_in.FragPosLightSpace); // wyh shadow值要么0要么1
    vec3 lighting = (ambient + (1.0 - shadow) * (diffuse + specular)) * color; // wyh 要么纯黑要么正常, 加上环境光的基础
    
    FragColor = vec4(lighting, 1.0);
}
// wyh 这个shader真正用于渲染地板和箱子的颜色, 包含了阴影(可见或不可见, 包含环境光)