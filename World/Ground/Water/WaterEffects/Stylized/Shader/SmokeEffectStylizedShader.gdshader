shader_type spatial;
render_mode blend_mix, depth_draw_opaque, cull_disabled, diffuse_toon, specular_toon;

uniform vec2 texture_scale = vec2(0.5, 0.5);
uniform sampler2D disolver_texture : source_color;

uniform float light_attenuation = 1.3;
uniform float shadow_roundness = 1.0;
uniform sampler2D color_1d_gradient : source_color, hint_default_black;


void fragment() {
	vec4 color = COLOR;
	float alpha = color.a;
	vec2 scale = texture_scale;
	vec2 pivot = vec2(0.00000, 0.00000);
	vec2 uv_scaling = (UV - pivot) * scale + pivot;

	ALBEDO = vec3(color.xyz);
	ALPHA = texture(disolver_texture, uv_scaling).r;
	ALPHA_SCISSOR_THRESHOLD = alpha;
}

void light() {
	float dot_result = dot(NORMAL, LIGHT);
	float add_some_math = (clamp(dot_result, -0.90, 0.90) + 1.0) * 0.47 * light_attenuation;
	float fresnel = pow(clamp(dot(NORMAL, VIEW), 0.0, 1.0), shadow_roundness);
	float round_cell_shade = clamp(add_some_math,0.04, 0.98) * fresnel;
	vec4 color_1D_gradient = texture(color_1d_gradient, vec2(round_cell_shade));
	DIFFUSE_LIGHT = clamp(LIGHT_COLOR * vec3(color_1D_gradient.xyz), vec3(0), vec3(1));
}